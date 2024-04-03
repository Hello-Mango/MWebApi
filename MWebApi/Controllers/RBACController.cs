using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RBACController : ControllerBase
    {
        public RBACController()
        {
        }
        [HttpGet]
        public string Test()
        {
            return "Test";
        }
    }
}
