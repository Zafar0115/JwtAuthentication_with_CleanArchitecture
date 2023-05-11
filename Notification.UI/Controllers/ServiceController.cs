using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;
using Notification.Domain.Models;

namespace Notification.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController :ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            Service? service = await _serviceRepository.GetByIdAsync(id);
            return Ok(service);
        }



        [HttpGet("GetAll")]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync(int page = 1, int pageSize = 10)
        {
            IQueryable<Service> services = await _serviceRepository.GetAllAsync();
            return Ok(services);
        }



        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] Service service)
        {
            bool isSuccess = await _serviceRepository.UpdateAsync(service);

            return Ok(isSuccess);
        }



        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isSuccess = await _serviceRepository.DeleteAsync(id);
            return Ok(isSuccess);
        }


        [HttpPost("Create")]
        [Route("[action]")]
        public async Task<IActionResult> CreateAsync([FromBody] Service service)
        {
            bool isSuccess = await _serviceRepository.CreateAsync(service);

            if (isSuccess)
                return Ok(service);

            return Ok(isSuccess);

        }
    }
}
