using Microsoft.IdentityModel.Tokens;
using QuickFireApi.Extensions.JWT;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuickFireApi.Extensions.Token
{
    public class MTokenHandler
    {
        private readonly IConfiguration _configuration;
        public MTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateAccessToken(string userId, string username, List<string> roleList, MJWTConfig mJWTConfig, IDictionary<string, object> claims = default!)
        {
            if (claims == null)
            {
                claims = new Dictionary<string, object>();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            string? secretKey = mJWTConfig.SecretKey;
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("SecretKey is null");
            }
            var key = Encoding.ASCII.GetBytes(secretKey!);
            var expires = mJWTConfig.Expires;
            var audience = mJWTConfig.Audience;
            var issuer = mJWTConfig.Issuer;
            List<Claim> claimsList = new List<Claim>();
            foreach (var role in roleList)
            {
                claimsList.Add(new Claim(ClaimTypes.Role, role));
            }
            claimsList.Add(new Claim(ClaimTypes.Name,username));
            claims.Add("UserId", userId);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsList),
                Expires = DateTime.UtcNow.AddMinutes(expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = audience,
                TokenType = "Bearer",
                Claims = claims,
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
            string? secretKey = section.GetValue<string>("SecretKey");
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("SecretKey is null");
            }
            var key = System.Text.Encoding.ASCII.GetBytes(secretKey);
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
