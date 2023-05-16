using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;
using Notification.Domain.Models;

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
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            UserRole? userRole = await _userRoleRepository.GetById(id);
            if (userRole == null) return NotFound("UserRole not found");
            return Ok(userRole);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            IQueryable<UserRole> userRoles = await _userRoleRepository.GetAll();
            return Ok(userRoles);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UserRole userRole)
        {
            bool isSuccess = await _userRoleRepository.UpdateAsync(userRole);


            return Ok(isSuccess);
        }



        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isSuccess = await _userRoleRepository.DeleteAsync(id);
            return Ok(isSuccess);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] UserRole userRole)
        {
            bool isSuccess = await _userRoleRepository.CreateAsync(userRole);

            if (isSuccess)
                return Ok(userRole);

            return Ok(isSuccess);

        }
    }
}
