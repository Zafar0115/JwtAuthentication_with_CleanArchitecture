using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System.Text.RegularExpressions;

namespace Notification.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            Message? message = await _messageRepository.GetByIdAsync(id);
            return Ok(message);
        }


        [HttpGet("getall")]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync(int page=1, int pageSize=10)
        {
            IQueryable<Message> messages = await _messageRepository.GetAllAsync();
            return Ok(messages);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] Message message)
        {
            bool isSuccess=await _messageRepository.UpdateAsync(message);

            return Ok(isSuccess);
        }



        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isSuccess= await _messageRepository.DeleteAsync(id);   
            return Ok(isSuccess);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] Message message)
        {
           bool isSuccess=await _messageRepository.CreateAsync(message);

            if (isSuccess)
                return Ok(message);

            return Ok(isSuccess);

        }


    }
}
