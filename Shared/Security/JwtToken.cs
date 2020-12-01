using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Shared.Security
{
    public interface IJwtToken
    {
        string GenerateToken(List<Claim> claims);
    }

    public class JwtToken : IJwtToken
    {
        private readonly IConfiguration _configuration;

        public JwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(List<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = _configuration["Jwt:Expires"] == null
                    ? DateTime.UtcNow.AddDays(1)
                    : DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration["Jwt:Expires"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}