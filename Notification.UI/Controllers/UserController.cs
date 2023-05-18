using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;
using Notification.Application.Services;
using Notification.Domain.Models;
using System.Data;

namespace Notification.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "UserGet")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            User? user = await _userRepository.GetById(id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "UserGetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            IQueryable<User> users = await _userRepository.GetAll();
            return Ok(users);
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "UserUpdate")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            bool isSuccess = await _userRepository.UpdateAsync(user);


            return Ok(isSuccess);
        }



        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "UserDelete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isSuccess = await _userRepository.DeleteAsync(id);
            return Ok(isSuccess);
        }


        [HttpPost("Create")]
        [Authorize(Roles = "UserCreate")]
        public async Task<IActionResult> CreateAsync([FromBody] User user)
        {
            bool isSuccess = await _userRepository.CreateAsync(user);

            if (isSuccess)
                return Ok(user);

            return Ok(isSuccess);

        }

    }
}
