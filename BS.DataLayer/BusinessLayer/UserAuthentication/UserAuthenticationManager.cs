using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BS.DataLayer.BusinessLayer.Repositories.UserAuthentication
{
    /// <summary>
    /// Represents class for user authentication
    /// </summary>
    public class UserAuthenticationManager : IUserAuthenticationManager
    {
        private readonly IConfiguration configuration;

        /// <summary>
        ///Constructor
        /// </summary>
        public UserAuthenticationManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Token generator functionality
        /// </summary>
        public async Task<string> GenerateToken(string userName, string indetityId, int profileId)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(userName, indetityId, profileId);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = this.configuration.GetSection("jwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(string userName, string indetityId, int profileId)
        {
            var claims = new List<Claim>
            {
                new Claim("UserName", userName),
                new Claim("IdentityId", indetityId),
                new Claim("ProfileId", profileId.ToString()),
            };

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = this.configuration.GetSection("JwtConfig");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(int.Parse(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
