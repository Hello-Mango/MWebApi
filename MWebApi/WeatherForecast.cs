namespace MWebApi
{
    /// <summary>
    /// 天气预测
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateOnly Date { get; set; }
        /// <summary>
        /// C温度
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// F温度
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 描述
        /// </summary>
        public string? Summary { get; set; }
    }
}
