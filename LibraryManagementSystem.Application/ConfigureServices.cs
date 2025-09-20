using FluentValidation;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Mappings;
using LibraryManagementSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraryManagementSystem.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
