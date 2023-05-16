using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;
using Notification.Domain.Models;

namespace Notification.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }


        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "ServiceGet")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            Service? service = await _serviceRepository.GetById(id);
            return Ok(service);
        }



        [HttpGet("GetAll")]
        [Authorize(Roles = "ServiceGetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            IQueryable<Service> services = await _serviceRepository.GetAll();
            return Ok(services);
        }



        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "ServiceUpdate")]

        public async Task<IActionResult> Update([FromBody] Service service)
        {
            bool isSuccess = await _serviceRepository.UpdateAsync(service);

            return Ok(isSuccess);
        }



        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "ServiceDelete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isSuccess = await _serviceRepository.DeleteAsync(id);
            return Ok(isSuccess);
        }


        [HttpPost("Create")]
        [Authorize(Roles ="ServiceCreate")]
        public async Task<IActionResult> CreateAsync([FromBody] Service service)
        {
            bool isSuccess = await _serviceRepository.CreateAsync(service);

            if (isSuccess)
                return Ok(service);

            return Ok(isSuccess);

        }
    }
}
