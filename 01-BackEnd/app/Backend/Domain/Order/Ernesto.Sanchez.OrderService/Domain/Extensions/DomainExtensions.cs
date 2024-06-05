using Ernesto.Sanchez.OrderService.Domain.Orders.Interfaces;
using Ernesto.Sanchez.OrderService.Domain.Orders.Services;

namespace Ernesto.Sanchez.OrderService.Domain.Extensions
{
    public static class DomainExtensions
    {
        public static void AddDomain(this IServiceCollection services)
        {            
            services.AddScoped<IOrderDomainService, OrderDomainService>();
        }
    }
}
