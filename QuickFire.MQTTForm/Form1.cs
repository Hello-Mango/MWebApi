using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using MQTTnet;
using MQTTnet.Client;
using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace QuickFire.MQTTForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TcpClient _client;
        IMqttClient client;
        MqttFactory factory = new MqttFactory();
        string Ipv4 = "";
        int Port = 1883;
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                if (client != null && client.IsConnected)
                {
                    client.DisconnectedAsync -= Client_DisconnectedAsync;
                    client.ConnectedAsync -= Client_ConnectedAsync;
                    client.ApplicationMessageReceivedAsync -= Client_ApplicationMessageReceivedAsync;
                    //await client.DisconnectAsync();
                    client.Dispose();
                }
                client = factory.CreateMqttClient();
                client.ApplicationMessageReceivedAsync += Client_ApplicationMessageReceivedAsync;
                client.ConnectedAsync += Client_ConnectedAsync;
                client.DisconnectedAsync += Client_DisconnectedAsync;
                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer(Ipv4, Port)
                    .WithClientId("winform" + Guid.NewGuid().ToString())
                    .Build();
                await client.ConnectAsync(options);
                return true;
            }
            catch (Exception ex)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.richTextBox1.AppendText(ex.Message + Environment.NewLine);
                });
                return false;
            }
        }

        private async Task Client_DisconnectedAsync(MqttClientDisconnectedEventArgs args)
        {
            await Task.Delay(3000);
            try
            {
                if (client.IsConnected == false)
                {
                    if (Active == true)
                    {
                        var options = new MqttClientOptionsBuilder()
                         .WithTcpServer(Ipv4, Port)
                         .WithClientId("winform" + Guid.NewGuid().ToString())
                         .Build();
                        await client.ConnectAsync(options);
                    }
                }
            }
            catch (Exception ex)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.richTextBox1.AppendText(ex.Message + Environment.NewLine);
                });
            }
        }

        private async Task Client_ConnectedAsync(MqttClientConnectedEventArgs args)
        {
            try
            {
                await client.SubscribeAsync("nodered_test", MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce);
            }
            catch (Exception ex)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.richTextBox1.AppendText(ex.Message + Environment.NewLine);
                });
            }
        }
        private async Task Client_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            await Task.Run(() =>
            {
                string messageText = Encoding.UTF8.GetString(arg.ApplicationMessage.PayloadSegment);
                string topic = arg.ApplicationMessage.Topic;
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.richTextBox1.AppendText(topic + " : " + messageText + Environment.NewLine);
                });
            });
        }
        bool Active = false;
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            Ipv4 = txtIP.Text;
            Active = true;
            //await ConnectAsync();
            _ = Task.Run(async () => { await DataReceive(); });
            //await TCPConnectAsync();
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            //client.ApplicationMessageReceivedAsync += Client_ApplicationMessageReceivedAsync;
            //client.ConnectedAsync += Client_ConnectedAsync;
            //client.DisconnectedAsync += Client_DisconnectedAsync;
            ////await client.DisconnectAsync();
            //client.Dispose();
            Active = false;
            _client.Close();
        }

        public async Task<bool> TCPConnectAsync()
        {
            try
            {
                if (Active == false)
                {
                    return false;
                }
                var ipEndPoint = new IPEndPoint(IPAddress.Parse(Ipv4), Port);
                _client = new TcpClient(AddressFamily.InterNetwork);
                //SetTcpKeepAlive(true, 3000, 1000);
                await _client.ConnectAsync(ipEndPoint.Address, ipEndPoint.Port);
                //if (_client.Connected)
                //{
                //    _ = Task.Run(async () => { await DataReceive(); });
                //}
                return _client.Connected;
            }
            catch (Exception ex)
            {
                this.richTextBox1.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.richTextBox1.AppendText(ex.Message + Environment.NewLine);
                });
                Thread.Sleep(3000);
                await TCPConnectAsync();
                return false;
            }
        }
        private byte[] tempBytes;
        int buffersize = 1024;
        private async Task DataReceive()
        {
            while (Active == true)
            {
                try
                {
                    if (_client == null || !_client.Connected)
                    {
                        // 如果连接断开，尝试重新连接
                        this.BeginInvoke((MethodInvoker)delegate ()
                        {
                            this.richTextBox1.AppendText("TcpClientProtocol DataReceive readCount is 0,may be connect is closed,start reconnect" + Environment.NewLine);
                        });
                        await TCPConnectAsync();
                    }
                    var stream = _client.GetStream();
                    var bytes = new byte[buffersize];
                    var readCount = await stream.ReadAsync(bytes, 0, buffersize);
                    if (readCount == 0)
                    {
                        this.BeginInvoke((MethodInvoker)delegate ()
                        {
                            this.richTextBox1.AppendText("TcpClientProtocol DataReceive readCount is 0,may be connect is closed,start reconnect" + Environment.NewLine);
                        });
                        await TCPConnectAsync();
                        continue;
                    }
                    tempBytes = new byte[readCount];
                    Array.Copy(bytes, tempBytes, readCount);
                    string value = Encoding.UTF8.GetString(tempBytes); 
                    this.richTextBox1.BeginInvoke((MethodInvoker)delegate ()
                    {
                        this.richTextBox1.AppendText(value + Environment.NewLine);
                    });
                }
                catch (Exception ex)
                {
                    this.BeginInvoke((MethodInvoker)delegate ()
                    {
                        this.richTextBox1.AppendText(ex.Message + Environment.NewLine);
                    });
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
