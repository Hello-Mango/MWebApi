using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QucikFire.Extensions;
using QuickFireApi.Core;
using QuickFireApi.Extensions.Token;
using QuickFireApi.Models.Request;

namespace QuickFireApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "Hello")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IStringLocalizer _stringLocalizer2;
        private readonly MTokenHandler _mTokenHandler;
        private readonly IGenerateId<long> idGenerateInterface1;
        private readonly ICacheService _cacheService;
        private readonly UserContext _userContext;
        public AccountController(MTokenHandler mTokenHandler,
            IStringLocalizer<AccountController> stringLocalizer,
            IStringLocalizer stringLocalizer2,
            IGenerateId<long> idGenerateInterface,
            UserContext userContext,
            ICacheService cacheService)
        {
            _userContext = userContext;
            _mTokenHandler = mTokenHandler;
            _stringLocalizer = stringLocalizer;
            _stringLocalizer2 = stringLocalizer2;
            idGenerateInterface1 = idGenerateInterface;
            _cacheService = cacheService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="_loginReq"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<TokenResponse> Login([FromBody] LoginReq _loginReq)
        {
            long text = idGenerateInterface1.NextId();
            string value = _stringLocalizer["Account"];
            string value2 = _stringLocalizer2["Account"];
            if (_loginReq.username == "admin" && _loginReq.password == "111111")
            {
                var token = _mTokenHandler.CreateAccessToken("admin", new List<string>()
                {
                    "admin",
                    "user"
                });
                var refreshToken = _mTokenHandler.CreateRefreshToken("admin");
                return new TokenResponse()
                {
                    AccessToken = $"bearer " + token,
                    RefreshToken = refreshToken,
                    Timestamp = DateTime.Now.Microsecond
                };
            }
            else
            {
                return new TokenResponse();
            }
        }
        /// <summary>
        /// 根据refreshtoken刷新token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public TokenResponse RefreshToken(string refreshToken)
        {
            return new TokenResponse()
            {
                RefreshToken = _userContext.UserName,
                Timestamp = DateTime.Now.Ticks
            };
        }
    }
}
