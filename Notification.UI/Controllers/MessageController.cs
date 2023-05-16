using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System.Text.RegularExpressions;

namespace Notification.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles="MessageGet")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            Message? message = await _messageRepository.GetById(id);
            return Ok(message);
        }


        [HttpGet("getall")]
        [Authorize(Roles = "MessageGetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            IQueryable<Message> messages = await _messageRepository.GetAll();
            return Ok(messages);
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "MessageUpdate")]
        public async Task<IActionResult> Update([FromBody] Message message)
        {
            bool isSuccess = await _messageRepository.UpdateAsync(message);

            return Ok(isSuccess);
        }



        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "MessageDelete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isSuccess = await _messageRepository.DeleteAsync(id);
            return Ok(isSuccess);
        }


        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "MessageCreate")]
        public async Task<IActionResult> Create([FromBody] Message message)
        {
            bool isSuccess = await _messageRepository.CreateAsync(message);

            if (isSuccess)
                return Ok(message);

            return Ok(isSuccess);

        }


    }
}
