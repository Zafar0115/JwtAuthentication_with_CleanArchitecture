﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notification.Application.Abstraction;
using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Notification.Application.Services
{
    public class TokenService:ITokenService
    {
        private readonly IApplicationDbContext _dbContext;

        public TokenService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string?> CreateTokenAsync(UserCredentials credentials, IConfiguration configuration)
        {
           



            User? user = _dbContext.Users.Where(o => o.UserName == credentials.UserName
                                        && o.EmailAddress == credentials.EmailAddress
                                        && o.Password == credentials.Password)
                                        .Include(u => u.UserRoles)
                                        .ThenInclude(x => x.Role)
                                        .ThenInclude(r=>r.RolePermissions)
                                        .ThenInclude(rp=>rp.Permission)
                                        .Select(x=>x)
                                        .FirstOrDefault();

            if (user == null) return null;

            IEnumerable<Role> userRoles = user.UserRoles.Select(x=>x.Role);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
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
            permissions=permissions.Distinct().ToList();

            foreach (string permission in permissions)
            {
                claims.Add(new Claim(ClaimTypes.Role, permission));
            }

           

            

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            SecurityToken token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(5),
                claims: claims,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;

        }


    }
}