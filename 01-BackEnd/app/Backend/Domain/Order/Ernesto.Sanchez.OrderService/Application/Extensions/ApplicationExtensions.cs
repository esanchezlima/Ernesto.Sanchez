using FluentValidation.AspNetCore;
using Ernesto.Sanchez.OrderService.Application.Interfaces;
using Ernesto.Sanchez.OrderService.Application.Mappers;
using Ernesto.Sanchez.OrderService.Application.Services;
using Ernesto.Sanchez.OrderService.Application.Validators;

namespace Ernesto.Sanchez.OrderService.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(OrderMapper));
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<OrderForCreationDtoValidator>());
            services.AddTransient<IValidationService, ValidationService>();
            services.AddScoped<ILibraryApplicationService, LibraryApplicationService>();

        }
    }
}
