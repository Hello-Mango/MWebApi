using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MWebApi.Extensions.Token
{
    public class MTokenHandler
    {
        private readonly IConfiguration _configuration;
        public MTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateAccessToken(string username, List<string> roleList)
        {
            var section = _configuration.GetSection("JWTConfig");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(section.GetValue<string>("SecretKey"));
            var expires = section.GetValue<int>("Expires");
            var audience = section.GetValue<string>("Audience");
            var issuer = section.GetValue<string>("Issuer");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddMinutes(expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = audience,
                TokenType = "Bearer",
                Claims = new Dictionary<string, object>()
                {
                    { ClaimTypes.Role, string.Join(',',roleList) }
                },
                Issuer = issuer,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        public string CreateRefreshToken(string username)
        {
            var section = _configuration.GetSection("JWTConfig");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(section.GetValue<string>("SecretKey"));
            var expires = section.GetValue<int>("Expires");
            var audience = section.GetValue<string>("Audience");
            var issuer = section.GetValue<string>("Issuer");


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddMinutes(expires).AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = audience,
                Issuer = issuer,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
