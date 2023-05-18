using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System.Data;

namespace Notification.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            Permission? permission = await _permissionRepository.GetById(id);
            if (permission == null) return NotFound("permission not found");
            return Ok(permission);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            IQueryable<Permission> permissions = await _permissionRepository.GetAll();
            return Ok(permissions);
        }

        [HttpPut]
        [Route("[action]")]

        public async Task<IActionResult> Update([FromBody] Permission permission)
        {
            bool isSuccess = await _permissionRepository.UpdateAsync(permission);


            return Ok(isSuccess);
        }



        [HttpDelete]
        [Route("[action]")]

        public async Task<IActionResult> Delete([FromBody] int id)
        {
            bool isSuccess = await _permissionRepository.DeleteAsync(id);
            return Ok(isSuccess);
        }


        [HttpPost("Create")]

        public async Task<IActionResult> CreateAsync([FromBody]Permission permission)
        {
            bool isSuccess = await _permissionRepository.CreateAsync(permission);

            if (isSuccess)
                return Ok(permission);

            return Ok(isSuccess);

        }
    }
}
