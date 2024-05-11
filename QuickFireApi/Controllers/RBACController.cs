using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QucikFire.Extensions;

namespace QuickFireApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RBACController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public RBACController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [AllowAnonymous]
        [HttpGet]
        public string Test()
        {
            _cacheService.Set("test", "test");
            return "Test";
        }
        [AllowAnonymous]
        [HttpGet]
        public string Get()
        {
            return _cacheService.Get("test");
        }
    }
}
