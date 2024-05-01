using DAL.VotingSystem.Entities.UserIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration,UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }

        public async Task<string> GenerateToken(ApplicationUser appUser)
        {
            var userRoles = await _userManager.GetRolesAsync(appUser);
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, appUser.Email),
        new Claim(ClaimTypes.GivenName, appUser.FirstName), 
        new Claim(ClaimTypes.NameIdentifier, appUser.Id), 
    };

            if (userRoles.Any())
            {
                var roleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
                claims.AddRange(roleClaims);
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Token:Issuer"],
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
