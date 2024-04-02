using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MWebApi.Extensions.Token;
using MWebApi.Models.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly MTokenHandler _mTokenHandler;
        public AccountController(MTokenHandler mTokenHandler, IStringLocalizer<AccountController> stringLocalizer)
        {
            _mTokenHandler = mTokenHandler;
            _stringLocalizer= stringLocalizer;
        }
        [AllowAnonymous]
        [HttpPost]
        public string Login([FromBody] LoginReq _loginReq)
        {
            string value = _stringLocalizer["Account"];
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
