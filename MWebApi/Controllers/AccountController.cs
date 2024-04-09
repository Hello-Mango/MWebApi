﻿using MCoreInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MWebApi.Core;
using MWebApi.Extensions.Token;
using MWebApi.Models.Request;

namespace MWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "Hello")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IStringLocalizer _stringLocalizer2;
        private readonly MTokenHandler _mTokenHandler;
        private readonly IdGenerateInterface<long> idGenerateInterface1;
        private readonly ICacheService _cacheService;
        public AccountController(MTokenHandler mTokenHandler,
            IStringLocalizer<AccountController> stringLocalizer,
            IStringLocalizer stringLocalizer2,
            IdGenerateInterface<long> idGenerateInterface,
            ICacheService cacheService)
        {
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
        public TokenResponse Login([FromBody] LoginReq _loginReq)
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
                    AccessToken = token,
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
        public TokenResponse RefreshToken(string refreshToken)
        {
            return new TokenResponse()
            {
                Timestamp = DateTime.Now.Microsecond
            };
        }
    }
}
