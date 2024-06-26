﻿using Microsoft.IdentityModel.Tokens;
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
        public string CreateAccessToken(string userId, string username, string tenantId, List<string> roleList)
        {
            var section = _configuration.GetSection("JWTConfig");
            var tokenHandler = new JwtSecurityTokenHandler();
            string? secretKey = section.GetValue<string>("SecretKey");
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("SecretKey is null");
            }
            var key = Encoding.ASCII.GetBytes(secretKey!);
            var expires = section.GetValue<int>("Expires");
            var audience = section.GetValue<string>("Audience");
            var issuer = section.GetValue<string>("Issuer");
            IDictionary<string, object> claims = new Dictionary<string, object>();
            foreach (var item in roleList)
            {
                claims.Add(ClaimTypes.Role, item);
            }
            claims.Add("UserId", userId);
            claims.Add("TenantId", tenantId);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
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
