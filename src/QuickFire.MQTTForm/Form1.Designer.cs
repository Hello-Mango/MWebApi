namespace QuickFire.MQTTForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox1 = new RichTextBox();
            btnDisConnect = new Button();
            btnConnect = new Button();
            txtIP = new TextBox();
            txtPort = new TextBox();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Bottom;
            richTextBox1.Location = new Point(0, 150);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(800, 300);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // btnDisConnect
            // 
            btnDisConnect.Location = new Point(694, 114);
            btnDisConnect.Name = "btnDisConnect";
            btnDisConnect.Size = new Size(94, 29);
            btnDisConnect.TabIndex = 1;
            btnDisConnect.Text = "DisConnect";
            btnDisConnect.UseVisualStyleBackColor = true;
            btnDisConnect.Click += btnDisConnect_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(594, 114);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(94, 29);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtIP
            // 
            txtIP.Location = new Point(100, 40);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(125, 27);
            txtIP.TabIndex = 3;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(328, 40);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(125, 27);
            txtPort.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtPort);
            Controls.Add(txtIP);
            Controls.Add(btnConnect);
            Controls.Add(btnDisConnect);
            Controls.Add(richTextBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button btnDisConnect;
        private Button btnConnect;
        private TextBox txtIP;
        private TextBox txtPort;
    }
}
