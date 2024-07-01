using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QucikFire.Extensions;
using QuickFire.Core;
using QuickFire.Extensions.Core;
using QuickFire.Utils;
using QuickFireApi.Extensions.Token;
using QuickFireApi.Models.Reponse;
using QuickFireApi.Models.Request;
using QuickFireApi.Response;

namespace QuickFireApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "System")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MTokenHandler _mTokenHandler;
        private readonly IUserContext _userContext;
        public AccountController(MTokenHandler mTokenHandler, IUserContext userContext)
        {
            _userContext = userContext;
            _mTokenHandler = mTokenHandler;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="_loginReq"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public LoginReponse Login([FromBody] LoginReq _loginReq)
        {
            if (_loginReq.username == "admin" && _loginReq.password == "111111")
            {
                var token = _mTokenHandler.CreateAccessToken("1", "admin", "2222", new List<string>()
                {
                    "admin",
                    "user"
                });
                var refreshToken = _mTokenHandler.CreateRefreshToken("admin");
                return new LoginReponse()
                {
                    AccessToken = token,
                    RefreshToken = refreshToken,
                    Timestamp = TimeUtils.GetTimeStamp(),
                    CompanyId = 1,
                    UserId = 1,
                    Username = "admin",
                };
            }
            else
            {
                return new LoginReponse();
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
                Timestamp = DateTimeOffset.UtcNow.Ticks
            };
        }
    }
}
