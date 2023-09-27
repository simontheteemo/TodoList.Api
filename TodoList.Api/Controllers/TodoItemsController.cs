using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TodoList.Api.DTOs;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoListService _service;
        private readonly ILogger<TodoItemsController> _logger;
        private readonly IMapper _mapper;

        public TodoItemsController(ITodoListService service, ILogger<TodoItemsController> logger, IMapper mapper )
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/TodoItems
        /// <summary>
        /// Get a list of Todo Items
        /// </summary>
        /// <returns>A list of Todo Items</returns>
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {   
            return Ok(await _service.GetTodoItems());
        }

        // GET: api/TodoItems/{id}
        /// <summary>
        /// Get a Todo Item by Id
        /// </summary>
        /// <param name="id">Todo Item Id</param>
        /// <returns>Todo Detail</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id)
        {
            var item = await _service.GetTodoItem(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return Ok (_mapper.Map<TodoListDto>(item));
            }
        }

        // PUT: api/TodoItems/{id}
        /// <summary>
        /// Mark a Todo item as completed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoListDto todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdateTodoItem(_mapper.Map<TodoItem>(todoItem));
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/TodoItems
        /// <summary>
        /// Create a Todo if it is not duplicating with any active Todo item
        /// </summary>
        /// <param name="todoItem"></param>
        /// <returns>TodoItem</returns>
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoListDto todoItem)
        {
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
            }
        } 
    }
}
