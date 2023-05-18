using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System.Data;

namespace Notification.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleController(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "UserRoleGet")]

        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            UserRole? userRole = await _userRoleRepository.GetById(id);
            if (userRole == null) return NotFound("UserRole not found");
            return Ok(userRole);
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "UserRoleGetAll")]

        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            IQueryable<UserRole> userRoles = await _userRoleRepository.GetAll();
            return Ok(userRoles);
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "UserRoleUpdate")]

        public async Task<IActionResult> Update([FromBody] UserRole userRole)
        {
            bool isSuccess = await _userRoleRepository.UpdateAsync(userRole);


            return Ok(isSuccess);
        }



        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "UserRoleDelete")]

        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isSuccess = await _userRoleRepository.DeleteAsync(id);
            return Ok(isSuccess);
        }


        [HttpPost("Create")]
        [Authorize(Roles = "UserRoleCreate")]

        public async Task<IActionResult> CreateAsync([FromBody] UserRole userRole)
        {
            bool isSuccess = await _userRoleRepository.CreateAsync(userRole);

            if (isSuccess)
                return Ok(userRole);

            return Ok(isSuccess);

        }
    }
}
