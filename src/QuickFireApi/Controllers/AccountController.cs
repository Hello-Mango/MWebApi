﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QucikFire.Extensions;
using QuickFire.Extensions.Interface;
using QuickFire.Core;
using QuickFireApi.Extensions.Token;
using QuickFireApi.Models.Reponse;
using QuickFireApi.Models.Request;

namespace QuickFireApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "System")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MTokenHandler _mTokenHandler;
        private readonly UserContext _userContext;
        public AccountController(MTokenHandler mTokenHandler, UserContext userContext)
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
                var token = _mTokenHandler.CreateAccessToken("admin", new List<string>()
                {
                    "admin",
                    "user"
                });
                var refreshToken = _mTokenHandler.CreateRefreshToken("admin");
                return new LoginReponse()
                {
                    AccessToken = $"bearer " + token,
                    RefreshToken = refreshToken,
                    Timestamp = DateTime.Now.Microsecond,
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
                Timestamp = DateTime.Now.Ticks
            };
        }
    }
}
