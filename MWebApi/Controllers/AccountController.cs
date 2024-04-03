using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MWebApi.Core;
using MWebApi.Extensions.Token;
using MWebApi.Models.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        public AccountController(MTokenHandler mTokenHandler, IStringLocalizer<AccountController> stringLocalizer, IStringLocalizer stringLocalizer2, IdGenerateInterface<long> idGenerateInterface)
        {
            _mTokenHandler = mTokenHandler;
            _stringLocalizer = stringLocalizer;
            _stringLocalizer2 = stringLocalizer2;
            idGenerateInterface1 = idGenerateInterface;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="_loginReq"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public string Login([FromBody] LoginReq _loginReq)
        {
            long text = idGenerateInterface1.NextId();
            string value = _stringLocalizer["Account"];
            string value2 = _stringLocalizer2["Account"];
            if (_loginReq.username == "admin" && _loginReq.password == "111111")
            {
                var token = _mTokenHandler.CreateToken("admin", new List<string>()
                {
                    "admin",
                    "user"
                });
                return token;
            }
            else
            {
                return "";
            }
        }
    }
}
