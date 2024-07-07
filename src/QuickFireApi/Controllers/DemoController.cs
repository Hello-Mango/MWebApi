using EventBusHandlers.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QucikFire.Extensions;
using QuickFire.Extensions.EventBus;
using QuickFire.Core;
using QuickFireApi.Extensions.Token;
using QuickFire.Extensions.Core;
using QuickFire.Domain.Shared;
using QuickFire.Infrastructure;
using QuickFire.Infrastructure.Repository;
using QuickFire.Application.Base;
using QuickFire.Domain.Entites;
using QuickFire.Application.DTOS.Request;
using QuickFire.Application.DTOS.Reponse;
using QuickFire.BizException;

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
        private readonly IRepository<SysUser> _repository;
        private readonly IUnitOfWork<SysDbContext> _unitOfWork;
        private readonly IUserService _userService;
        private readonly SysDbContext _applicationDbContext;
        public DemoController(
            IStringLocalizer<AccountController> stringLocalizer,
            IStringLocalizer stringLocalizer2,
            IGenerateId<long> idGenerateInterface,
            IEventPublisher eventBus,
            IUnitOfWork<SysDbContext> unitOfWork,
            IRepository<SysUser> repository,
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
            IRepository<SysUser> repository1 = new LongIdRepository<SysUser>(_applicationDbContext);
            var respository = _unitOfWork.GetLongRepository<SysUser>();
            respository.Add(new SysUser() { Id = idGenerateInterface1.NextId(), No = "111", Password = "333", Name = "test", Email = "111", Mobile = "2222" });
            var item = respository.FindBy(z => z.CreatedAt > DateTime.UtcNow);
            _cacheService.Set("test", item);
            return "Test";
        }
        [HttpGet]
        public SysUser? GetCache()
        {
            _userService.CreateUser(new SysUser() { Id = idGenerateInterface1.NextId(), Name = "tes222t" });
            return _cacheService.Get<SysUser>("test");
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

        [HttpDelete]
        public async Task Delete(SingleReq<long> singleReq)
        {
            await _userService.DeleteAsync(singleReq._);
        }

        [HttpPut("{id}")]
        public async Task<UserResp> Modify([FromRoute] long id, UserReq userReq)
        {
            var respository = _unitOfWork.GetLongRepository<SysUser>();
            var user = await respository.FindByIdAsync(id);
            if (user == null)
            {
                throw new EnumException(ExceptionEnum.USER_NOT_FOUND, id.ToString());
            }
            user.SetEmail(userReq.Email);
            user.SetMobile(userReq.Mobile);
            await _userService.UpdateAsync(user);
            return new UserResp()
            {
                Id = user.Id,
                Name = user.Name,
                No = user.No,
                Email = user.Email,
                Mobile = user.Mobile,
                IsLock = user.IsLock
            };
        }

        [HttpGet]
        public async Task<List<UserResp>> Get()
        {
            var res = await _userService.GetAllAsync();
            return res.Select(z => new UserResp()
            {
                Id = z.Id,
                Name = z.Name,
                No = z.No,
                Email = z.Email,
                Mobile = z.Mobile,
                IsLock = z.IsLock
            }).ToList();
        }

        [HttpPost]
        public async Task<UserResp> Add(UserReq userReq)
        {
            SysUser sysUser = new SysUser()
            {
                Id = idGenerateInterface1.NextId(),
                Name = userReq.Name,
                No = userReq.No,
                Password = userReq.Password,
                Email = userReq.Email,
                Mobile = userReq.Mobile
            };
            var res = await _userService.CreateAsync(sysUser);
            return new UserResp()
            {
                Id = res.Id,
                Name = res.Name,
                No = res.No,
                Email = res.Email,
                Mobile = res.Mobile,
                IsLock = res.IsLock
            };
        }
    }
}
