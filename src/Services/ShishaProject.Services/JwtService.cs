namespace ShishaProject.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.JsonWebTokens;
    using Microsoft.IdentityModel.Tokens;
    using ShishaProject.Common.Helpers;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Interfaces;

    public class JwtService : IJwtService
    {
        private readonly IOptionsSnapshot<JwtConfig> config;

        public JwtService(IOptionsSnapshot<JwtConfig> config)
        {
            this.config = config;
        }

        public string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config.Value.Key));
            var tokenHandler = new JsonWebTokenHandler();

            var jwt = new SecurityTokenDescriptor
            {
                Issuer = this.config.Value.Issuer,
                Audience = this.config.Value.Audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddYears(5),
            };

            string jws = tokenHandler.CreateToken(jwt);
            return jws;
        }
    }
}
