using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Application.Interfaces;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.QueryParametersTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.EndpointHandlers
{
    public static class OrdersCollectionHandlers
    {
        public static async Task<Results<BadRequest, NotFound, Ok<IEnumerable<OrderDto>>>> GetOrdersCollectionAsync(
            [FromServices] ILibraryApplicationService _libraryApplicationService,
            [FromQuery] QueryParameterListGuidsType ordersIds
        )
        {            
            if (ordersIds == null || !ordersIds.Items.Any())
            {
                return TypedResults.BadRequest();
            }

            var result = await _libraryApplicationService.GetOrdersCollectionAsync(ordersIds.Items);
            //var result = new GetOrderCollectionResult();

            if (!result.OrdersFound)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result.OrderCollection);

        }
        public static async Task<Results<BadRequest, CreatedAtRoute<IEnumerable<OrderDto>>>> CreateOrdersCollectionAsync(
            [FromServices] ILibraryApplicationService _libraryApplicationService,
            [FromBody] IEnumerable<OrderForCreationDto> orderCollection
        )
        {
            if (orderCollection == null)
            {
                return TypedResults.BadRequest();
            }

            var result = await _libraryApplicationService.CreateOrderCollectionAsync(orderCollection);            

            return TypedResults.CreatedAtRoute(result.OrdersCollection, $"GetOrdersCollection", new { ordersIds = result.OrdersIdsAsString });
        }
    }
}
