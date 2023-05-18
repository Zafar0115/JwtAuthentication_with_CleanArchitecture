using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notification.Application.Abstraction;
using Notification.Application.Extensions;
using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Notification.Application.Services
{
    public class JwtManagerRepository : IJwtManagerRepository
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public JwtManagerRepository(IConfiguration configuration, IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public Token? GenerateRefreshToken(UserCredentials credentials)
        {
            User? user = _dbContext.Users.Where(o => o.UserName == credentials.UserName
                                        && o.EmailAddress == credentials.EmailAddress
                                        && o.Password == credentials.Password)?
                                        .Include(u => u.UserRoles)?
                                        .ThenInclude(x => x.Role)?
                                        .ThenInclude(r => r.RolePermissions)
                                        .ThenInclude(rp => rp.Permission)
                                        .Select(x => x)
                                        .FirstOrDefault();

            if (user == null) return null;

            IEnumerable<Role?> userRoles = user.UserRoles.Select(x => x.Role);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.FullName)
            };
            List<string> permissions = new List<string>();

            foreach (UserRole role in user.UserRoles)
            {
                foreach (RolePermission permission in role.Role.RolePermissions)
                {
                    permissions.Add(permission.Permission.PermissionName);
                }
            }

            permissions = permissions.Distinct().ToList();

            foreach (string permission in permissions)
            {
                claims.Add(new Claim(ClaimTypes.Role, permission));
            }


            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            int.TryParse(_configuration["Jwt:RefreshTokenLifetime"], out int lifetime);

            SecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(lifetime),
                claims: claims,
                signingCredentials: signingCredentials
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            return new Token { AccessToken = accessToken, RefreshToken = refreshToken };
        }
        public Token? GenerateAccessTokens(UserCredentials credentials)
        {

            User? user = _dbContext.Users.Where(o => o.UserName == credentials.UserName
                                                    && o.EmailAddress == credentials.EmailAddress
                                                    && o.Password == credentials.Password.ComputeHash())?
                                                    .Include(u => u.UserRoles)?
                                                    .ThenInclude(x => x.Role)?
                                                    .ThenInclude(r => r.RolePermissions)
                                                    .ThenInclude(rp => rp.Permission)
                                                    .Select(x => x)
                                                    .FirstOrDefault();

            if (user == null) return null;

            IEnumerable<Role?> userRoles = user.UserRoles.Select(x => x.Role);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.FullName)
            };
            List<string> permissions = new List<string>();

            foreach (UserRole role in user.UserRoles)
            {
                foreach (RolePermission permission in role.Role.RolePermissions)
                {
                    permissions.Add(permission.Permission.PermissionName);
                }
            }

            permissions = permissions.Distinct().ToList();

            foreach (string permission in permissions)
            {
                claims.Add(new Claim(ClaimTypes.Role, permission));
            }


            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            int.TryParse(_configuration["Jwt:AccessTokenLifetime"], out int lifetime);

            SecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(lifetime),
                claims: claims,
                signingCredentials: signingCredentials
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            return new Token { AccessToken = accessToken, RefreshToken = refreshToken };
        }
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidIssuer = _configuration["Jwt:Issuer"],
                RequireExpirationTime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return principal;

        }
       
        private string GenerateRefreshToken()
        {
            var randomNumber = new Byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
