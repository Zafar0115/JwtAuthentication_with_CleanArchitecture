using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System.Data;

namespace Notification.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionController(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "RolePermissionGet")]

        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            RolePermission? rolePermission = await _rolePermissionRepository.GetById(id);
            if (rolePermission == null) return NotFound("rolePermission not found");
            return Ok(rolePermission);
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "RolePermissionGetAll")]

        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            IQueryable<RolePermission> rolePermissions = await _rolePermissionRepository.GetAll();
            return Ok(rolePermissions);
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "RolePermissionUpdate")]

        public async Task<IActionResult> Update([FromBody] RolePermission rolePermission)
        {
            bool isSuccess = await _rolePermissionRepository.UpdateAsync(rolePermission);


            return Ok(isSuccess);
        }



        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "RolePermissionDelete")]

        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isSuccess = await _rolePermissionRepository.DeleteAsync(id);
            return Ok(isSuccess);
        }


        [HttpPost("Create")]
        [Authorize(Roles = "RolePermissionCreate")]

        public async Task<IActionResult> CreateAsync([FromBody] RolePermission rolePermission)
        {
            bool isSuccess = await _rolePermissionRepository.CreateAsync(rolePermission);

            if (isSuccess)
                return Ok(rolePermission);

            return Ok(isSuccess);

        }
    }
}
