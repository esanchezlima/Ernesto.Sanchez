using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.DataMapping.TypeHelper;
using Library.Service.Infrastructure.Http.Helpers.LinksBuilders;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Extensions
{
    public static class HttpExtensions
    {
        public static void AddHttp(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddHttpContextAccessor();
            services.Configure<JsonOptions>(options => { options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase; });
            services.AddSwaggerDocumentation();
            //services.AddSwaggerGen();
            
            services.AddScoped<ITypeHelperService, TypeHelperService>();
            services.AddScoped<IOrderLinksBuilder, OrderLinksBuilder>();
            services.AddScoped<IItemLinksBuilder, ItemLinksBuilder>();
            services.AddScoped<IRootLinksBuilder, RootLinksBuilder>();
        }
    }
}
