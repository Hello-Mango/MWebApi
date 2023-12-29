using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MWebApi.Models.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public string Login([FromBody] LoginReq _loginReq)
        {
            if (_loginReq.username == "admin" && _loginReq.password == "111111")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = System.Text.Encoding.ASCII.GetBytes("your_secret_key_here");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", "123") }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Audience = "your_audience_here",
                    Claims = new Dictionary<string, object>()
                    {
                        { "role", "admin" }
                    },
                    Issuer = "your_issuer_here"
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return tokenString;
            }
        }
    }
}
