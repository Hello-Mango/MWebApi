using EventBusHandlers.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QucikFire.Extensions;
using QuickFire.Extensions.EventBus;
using QuickFire.Extensions.Interface;
using QuickFire.Core;
using QuickFireApi.Extensions.Token;
using QuickFireApi.Models.Request;

namespace QuickFireApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IStringLocalizer _stringLocalizer2;
        private readonly IGenerateId<long> idGenerateInterface1;
        private readonly ICacheService _cacheService;
        private readonly IEventPublisher _eventBus;
        public DemoController(
            IStringLocalizer<AccountController> stringLocalizer,
            IStringLocalizer stringLocalizer2,
            IGenerateId<long> idGenerateInterface,
            IEventPublisher eventBus,
            ICacheService cacheService)
        {
            _stringLocalizer = stringLocalizer;
            _stringLocalizer2 = stringLocalizer2;
            idGenerateInterface1 = idGenerateInterface;
            _cacheService = cacheService;
            _eventBus = eventBus;
        }
        [HttpPost]
        public string AddCache(SingleReq<long> singleReq)
        {
            _cacheService.Set("test", singleReq._);
            return "Test";
        }
        [HttpGet]
        public string? GetCache()
        {
            return _cacheService.Get("test");
        }
        [HttpGet]
        public string GetStringLocalizer()
        {
            return _stringLocalizer["Account"];
        }
        [HttpGet]
        public string GetStringLocalizer2()
        {
            return _stringLocalizer2["Account"];
        }
        [HttpGet]
        public long GetId()
        {
            return idGenerateInterface1.NextId();
        }
        [HttpPost]
        public async Task PublishEvent()
        {
            await _eventBus.PublishAsync(new Test());
        }
    }
}
