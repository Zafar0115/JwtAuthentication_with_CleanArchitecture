using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;
using Notification.Domain.Models;

namespace Notification.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            Role? Role = await _roleRepository.GetById(id);
            if (Role == null) return NotFound("Role not found");
            return Ok(Role);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            IQueryable<Role> roles = await _roleRepository.GetAll();
            return Ok(roles);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] Role role)
        {
            bool isSuccess = await _roleRepository.UpdateAsync(role);


            return Ok(isSuccess);
        }



        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isSuccess = await _roleRepository.DeleteAsync(id);
            return Ok(isSuccess);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] Role role)
        {
            bool isSuccess = await _roleRepository.CreateAsync(role);

            if (isSuccess)
                return Ok(role);

            return Ok(isSuccess);

        }
    }
}
