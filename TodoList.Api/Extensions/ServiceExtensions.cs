﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Core.Interfaces;
using TodoList.Core.Repositories;
using TodoList.Core.Services;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Respositories;

namespace TodoList.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {

            // Domain
            services.AddScoped<ITodoListService, TodoListService>();
            services.AddScoped<ITodoListRepository, TodoListRespository>();

            // Infra
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoItemsDB"));
        }
    }
}
