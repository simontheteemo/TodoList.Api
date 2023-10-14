using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Core.Interfaces;
using TodoList.Core.Interfaces.Clients;
using TodoList.Core.Repositories;
using TodoList.Core.Services;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Repositories;
using TodoList.Infrastructure.Sqs;

namespace TodoList.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {

            // Domain
            services.AddHostedService<SQSPollingService>();
            services.AddScoped<ITodoListService, TodoListService>();
            services.AddScoped<ITodoListRepository, TodoListRepository>();
            services.AddScoped<IXeroSqsService, XeroSqsService>();
            services.AddScoped<IXeroSqsClient, XeroSqsClient>();

            // Infra
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoItemsDB"));
        }
    }
}
