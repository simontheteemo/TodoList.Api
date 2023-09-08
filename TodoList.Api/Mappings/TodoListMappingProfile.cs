using AutoMapper;
using TodoList.Core.Models;
using TodoList.Api.DTOs;

namespace TodoList.Api.Mappings;
public class TodoListMappingProfile : Profile
{
    public TodoListMappingProfile()
    {
        CreateMap<TodoItem, TodoListDto>().ReverseMap();
    }
}