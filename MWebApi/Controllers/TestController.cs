using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Numerics;

namespace MWebApi.Controllers
{
    [ApiExplorerSettings(GroupName = "Hello")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 获取天气信息
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet(Name = "GetWeatherForecast1")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
            });
        }
    }
}
