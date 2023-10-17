using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TodoList.Api.DTOs;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;
using TodoList.Core.Services;
using TodoList.Infrastructure.Sqs;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XeroTaxController : ControllerBase
    {
        private readonly ITodoListService _service;
        private readonly IXeroSqsService _xeroSqsService;
        private readonly ILogger<XeroTaxController> _logger;
        private readonly IMapper _mapper;

        public XeroTaxController(ITodoListService service, ILogger<XeroTaxController> logger, IMapper mapper, IXeroSqsService xeroSqsService)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
            _xeroSqsService = xeroSqsService;
        }

        // POST: api/TodoItems
        /// <summary>
        /// Create a Todo if it is not duplicating with any active Todo item
        /// </summary>
        /// <param name="todoItem"></param>
        /// <returns>TodoItem</returns>
        [HttpPost]
        public IActionResult PostTodoItem(TodoListDto todoItem)
        {
            /*
            if (string.IsNullOrEmpty(todoItem?.Description))
            {
                return BadRequest("Description is required");
            }
            try
            {
                return Ok(await _service.CreateTodoItem(_mapper.Map<TodoItem>(todoItem)));
            }
            catch(Exception ex) { 
                return BadRequest(ex.Message);
            }*/
            return Ok(_xeroSqsService.SendMessageToSQS(_mapper.Map<TodoItem>(todoItem)));
        }
    }
}
