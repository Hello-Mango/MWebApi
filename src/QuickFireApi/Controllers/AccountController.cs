using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QucikFire.Extensions;
using QuickFire.Application.Base;
using QuickFire.Application.DTOS.Reponse;
using QuickFire.Application.DTOS.Request;
using QuickFire.Core;
using QuickFire.Extensions.Core;
using QuickFire.Infrastructure;
using QuickFire.Utils;
using QuickFireApi.Extensions.JWT;
using QuickFireApi.Extensions.Token;

namespace QuickFireApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "System")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MTokenHandler _mTokenHandler;
        private readonly ISessionContext _sessionContext;
        private readonly JWTConfig _jwtConfig;
        private readonly IUserService _userService;
        public AccountController(MTokenHandler mTokenHandler, ISessionContext sessionContext, IOptions<AppSettings> appsettings, IUserService userService)
        {
            _sessionContext = sessionContext;
            _mTokenHandler = mTokenHandler;
            _jwtConfig = appsettings.Value.JWTConfig;
            _userService = userService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="_loginReq"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<LoginReponse> Login([FromBody] LoginReq _loginReq)
        {
            var user = await _userService.CheckLoginSync(_loginReq);
            MJWTConfig mJWTConfig = new MJWTConfig()
            {
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer,
                SecretKey = _jwtConfig.SecretKey,
                Expires = _jwtConfig.Expires,
                RefreshExpiration = _jwtConfig.RefreshExpiration
            };
            var token = _mTokenHandler.CreateAccessToken(user.Id.ToString(), user.Name, new List<string>()
                {
                    "admin",
                    "user"
                }, mJWTConfig);
            var refreshToken = _mTokenHandler.CreateRefreshToken(user.Name);
            return new LoginReponse()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                Timestamp = TimeUtils.GetTimeStamp(),
                UserId = user.Id,
                Username = user.Name,
            };
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
                RefreshToken = _sessionContext.UserName,
                Timestamp = DateTimeOffset.UtcNow.Ticks
            };
        }
    }
}
