using EventBusHandlers.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QucikFire.Extensions;
using QuickFire.Extensions.EventBus;
using QuickFire.Core;
using QuickFireApi.Extensions.Token;
using QuickFireApi.Models.Request;
using QuickFire.Extensions.Core;
using QuickFire.Domain.Shared;
using QuickFire.Infrastructure;
using QuickFire.Domain.Entity;
using QuickFire.Infrastructure.Repository;
using QuickFire.Application.Interface;

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
        private readonly ILogger _logger;
        private readonly IRepository<TUser> _repository;
        private readonly IUnitOfWork<SysDbContext> _unitOfWork;
        private readonly IUserService _userService;
        private readonly SysDbContext _applicationDbContext;
        public DemoController(
            IStringLocalizer<AccountController> stringLocalizer,
            IStringLocalizer stringLocalizer2,
            IGenerateId<long> idGenerateInterface,
            IEventPublisher eventBus,
            IUnitOfWork<SysDbContext> unitOfWork,
            IRepository<TUser> repository,
            SysDbContext applicationDbContext, IUserService userService,
            ICacheService cacheService)
        {
            _stringLocalizer = stringLocalizer;
            _stringLocalizer2 = stringLocalizer2;
            idGenerateInterface1 = idGenerateInterface;
            _cacheService = cacheService;
            _eventBus = eventBus;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _applicationDbContext = applicationDbContext;
            _userService = userService;
        }
        [HttpPost]
        public string AddCache(SingleReq<long> singleReq)
        {
            IRepository<TUser> repository1 = new LongIdRepository<TUser>(_applicationDbContext);
            var respository = _unitOfWork.GetRepository<TUser>();
            respository.Add(new TUser() { Id = idGenerateInterface1.NextId(), Name = "test" });
            var item = respository.FindBy(z => z.CreatedAt > DateTime.UtcNow);
            _cacheService.Set("test", item);
            return "Test";
        }
        [HttpGet]
        public TUser? GetCache()
        {
            _userService.CreateUser(new TUser() { Id = idGenerateInterface1.NextId(), Name = "tes222t" });
            return _cacheService.Get<TUser>("test");
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
