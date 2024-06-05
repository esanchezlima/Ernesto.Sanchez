using Ernesto.Sanchez.OrderService.Domain.Orders.Interfaces;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Contexts;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.DataMapping.ModelMapping;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Repositories;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Extension
{
    public class PersistenceOptions
    {
        public string ConnectionString { get; set; }
    }

    public static class PersistenceExtension
    {
        public static void UseSeedData(this IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopedFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrderContext>();
                context.EnsureSeedDataForContext();
            }
        }
        public static void AddPersistence(this IServiceCollection services, Action<PersistenceOptions> configure)
        {
            var options = new PersistenceOptions();
            configure(options);

            services.AddDbContext<OrderContext>(o => o.UseSqlServer(options.ConnectionString));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<OrderUnitOfWork>();
            services.AddTransient<IOrderPropertyMappingService, OrderPropertyMappingService>();


        }
    }
}
