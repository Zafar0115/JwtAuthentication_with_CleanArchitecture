using Microsoft.AspNetCore.Authorization;
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
        private readonly IJwtManagerRepository _jwtManagerRepository;
        private readonly IUserRefreshTokenService _userRefreshTokenService;

        private readonly IConfiguration _configuration;

        public LoginController
            (
            IApplicationDbContext dbContext,
            ITokenService tokenService,
            IConfiguration configuration,
            IJwtManagerRepository jwtManagerRepository,
            IUserRefreshTokenService userRefreshTokenService
            )
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _configuration = configuration;
            _jwtManagerRepository = jwtManagerRepository;
            _userRefreshTokenService = userRefreshTokenService;
        }

        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] UserCredentials credentials)
        {
            Token? token = _jwtManagerRepository.GenerateTokens(credentials);

            if (token is null) return Results.NotFound("Invalid attempt");

            User? user = _dbContext.Users.FirstOrDefault(x => x.UserName == credentials.UserName &&
                                                         x.Password == credentials.Password &&
                                                         x.EmailAddress == credentials.EmailAddress);

            if (user is null) Results.NotFound("Invalid attempt");

            UserRefreshTokens obj = new UserRefreshTokens()
            {
                User = user,
                RefreshToken = token.RefreshToken
            };

            _userRefreshTokenService.AddUserRefreshTokens(obj);
            return Results.Ok(token);


        }
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] Token token)
        {
            var principal = _jwtManagerRepository
                            .GetPrincipalFromExpiredToken(token.AccessToken);

            var username = principal?.Identity.Name;

            User? user = _dbContext.Users
                            .FirstOrDefault(x => x.UserName == username);

            if (user is null)
                return Unauthorized("Invalid attempt");

            UserCredentials credentials = new()
            {
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                UserName = user.UserName
            };

            UserRefreshTokens? savedRefreshedToken = await _userRefreshTokenService
                                                    .GetSavedRefreshTokens(credentials, token.RefreshToken);

            if (savedRefreshedToken is null||savedRefreshedToken.RefreshToken!=token.RefreshToken)
            {
                return Unauthorized("Invalid attempt");
            }


            var newToken = _jwtManagerRepository
                .GenerateRefreshToken(credentials);
            if (newToken == null)
            {
                return Unauthorized("Invalid attempt");
            }

            UserRefreshTokens obj = new UserRefreshTokens
            {
                RefreshToken = newToken.RefreshToken,
                User = user
            };

            await _userRefreshTokenService.DeleteUserRefreshTokens(credentials, token.RefreshToken);
            await _userRefreshTokenService.AddUserRefreshTokens(obj);


            return Ok(newToken);



        }
    }
}
