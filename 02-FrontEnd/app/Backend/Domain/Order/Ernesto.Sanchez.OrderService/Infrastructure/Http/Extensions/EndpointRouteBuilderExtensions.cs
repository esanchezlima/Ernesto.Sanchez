using Ernesto.Sanchez.OrderService.Infrastructure.Http.EndpointHandlers;
using Microsoft.AspNetCore.Authorization;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterOrdersEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var ordersEndpoints = endpointRouteBuilder
                .MapGroup("api/orders")
                .WithTags("Orders") 
              .RequireAuthorization();

            ordersEndpoints.MapGet("", OrdersHandlers.GetOrdersAsync)
                .WithName("GetOrders")
                .WithOpenApi() 
              .RequireAuthorization(new AuthorizeAttribute { Roles = "realm-role" });
            ordersEndpoints.MapGet("/{orderId:guid}", OrdersHandlers.GetOrderByOrderIdAsync)
                .WithName("GetOrder")
                .WithOpenApi() 
                .RequireAuthorization(new AuthorizeAttribute { Roles = "client-role" });
            ordersEndpoints.MapPost("", OrdersHandlers.CreateOrderAsync)
                .ProducesValidationProblem(422)
                .WithName("CreateOrder")
                .WithOpenApi();

            ordersEndpoints.MapPost("/WithDateofOrder", OrdersHandlers.CreateOrderWithDateofOrderAsync)
                .ProducesValidationProblem(422)
                .WithName("CreateOrderWithDateofOrder")
                .WithOpenApi();

            ordersEndpoints.MapDelete("/{orderId:guid}", OrdersHandlers.DeleteOrderAsync)
                .WithName("DeleteOrder")
                .WithOpenApi();

            ordersEndpoints.MapPut("/{orderId:guid}", OrdersHandlers.UpdateOrderAsync)
                .ProducesValidationProblem(422)
                .WithName("UpdateOrder")
                .WithOpenApi();
        }
        public static void RegisterOrderCollectionEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var ordersCollectionEndpoints = endpointRouteBuilder
                .MapGroup("api/orderscollection")
                .WithTags("OrderCollection");

            ordersCollectionEndpoints.MapGet("", OrdersCollectionHandlers.GetOrdersCollectionAsync)
                .WithName("GetOrderCollection")
                .WithOpenApi();

            ordersCollectionEndpoints.MapPost("", OrdersCollectionHandlers.CreateOrdersCollectionAsync)
                .WithName("CreateOrderCollection")
                .WithOpenApi();
        }
        public static void RegisterItemsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var itemsEndpoints = endpointRouteBuilder
                .MapGroup("api/orders/{orderId:guid}/items")
                .WithTags("Items");

            itemsEndpoints.MapGet("", ItemsHandlers.GetItemsForOrderAsync)
                .WithName("GetItemsForOrder")
                .WithOpenApi();

            itemsEndpoints.MapGet("/{itemId:guid}", ItemsHandlers.GetItemByItemIdForOrderAsync)
                .WithName("GetItemForOrder")
                .WithOpenApi();

            itemsEndpoints.MapPost("", ItemsHandlers.CreateItemForOrderAsync)
                .ProducesValidationProblem(422)
                .WithName("CreateItemForOrder")
                .WithOpenApi();

            itemsEndpoints.MapDelete("/{itemId:guid}", ItemsHandlers.DeleteItemForOrderAsync)
               .WithName("DeleteItemForOrder")
               .WithOpenApi();

            itemsEndpoints.MapPut("/{itemId:guid}", ItemsHandlers.UpdateItemForOrderAsync)
                .ProducesValidationProblem(422)
                .WithName("UpdateItemForOrder")
                .WithOpenApi();

            itemsEndpoints.MapPatch("/{itemId:guid}", ItemsHandlers.PartiallyUpdateItemForOrderAsync)
                .ProducesValidationProblem(422)
                .WithName("PartiallyUpdateItemForOrder")
                .WithOpenApi();
        }
        public static void RegisterRootEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var rootEndpoints = endpointRouteBuilder.MapGroup("api").WithTags("Root");

            rootEndpoints.MapGet("", RootHandlers.GetRootAsync)
                .WithName("GetRoot")
                .WithOpenApi();
        }
        public static void RegisterEndpoints(this IEndpointRouteBuilder app)
        {
            app.RegisterRootEndpoints();
            app.RegisterOrdersEndpoints();
            app.RegisterOrderCollectionEndpoints();
            app.RegisterItemsEndpoints();
        }
        //public static void RegisterEndpoints(this IEndpointRouteBuilder app)
        //{
        //    app.RegisterOrdersEndpoints();
        //}
    }
}
