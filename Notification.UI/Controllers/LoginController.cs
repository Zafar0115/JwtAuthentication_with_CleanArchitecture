using Microsoft.AspNetCore.Mvc;
using Notification.Application.Abstraction;
using Notification.Application.Interfaces;
using Notification.Domain.Models;

namespace Notification.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public LoginController(IApplicationDbContext dbContext, ITokenService tokenService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] UserCredentials credentials)
        {
            string? accessToken = await _tokenService.CreateTokenAsync(credentials, _configuration);

            if (accessToken == null) return Results.NotFound("User not found");

            return Results.Ok(
                new Token()
                {
                    AccessToken = accessToken,
                    RefreshToken = "aaa1234"
                });
        }
    }
}
