using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Application.Interfaces;
using Ernesto.Sanchez.OrderService.Application.Services;
using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Extensions;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.DataMapping.ModelMapping;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.DataMapping.TypeHelper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text.Json;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.EndpointHandlers
{
    public static class OrdersHandlers
    {
        public static async Task<Results<BadRequest, Ok<IEnumerable<ExpandoObject>>, Ok<LinkedCollectionResource>,  Ok<List<OrderDto>>>> GetOrdersAsync(
            [FromServices] ILibraryApplicationService _orderApplicationService,
            [FromServices] IOrderPropertyMappingService _orderPropertyMappingService,
            [FromServices] ITypeHelperService _typeHelperService,
            [FromServices] IHttpContextAccessor _httpContextAccessor,
            [FromHeader(Name = "Accept")] string? mediaType,
            [AsParameters] OrdersResourceParameters ordersResourceParameters
        )
        {
            if (!_orderPropertyMappingService.ValidMappingExistsFor(ordersResourceParameters.OrderBy) || !_typeHelperService.TypeHasProperties<OrderDto>(ordersResourceParameters.Fields))
            {
                return TypedResults.BadRequest();
            }
            var result = await _orderApplicationService.GetOrdersAsync(ordersResourceParameters);
            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.PaginationMetadata, new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));

            switch (mediaType)
            {
                case "application/vnd.ernesto.hateoas+json":
                    return TypedResults.Ok(result.LinkedCollectionResource);
                default:
                    return TypedResults.Ok(result.ShapedOrders);
            }
        }
        public static async Task<Results<BadRequest,NotFound, Ok<ExpandoObject>, Ok<IDictionary<string, object>>, Ok<OrderDto>>> GetOrderByOrderIdAsync(
           [FromServices] ILibraryApplicationService _orderApplicationService,
           [FromServices] ITypeHelperService _typeHelperService,
           [FromHeader(Name = "Accept")] string? mediaType,
           [FromQuery] string? fields,
           Guid orderId
        )
        {
            if (!_typeHelperService.TypeHasProperties<OrderDto>(fields))
            {
                return TypedResults.BadRequest();
            }
            var result = await _orderApplicationService.GetOrderByOrderIdAsync(orderId,fields);

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            switch (mediaType)
            {
                case "application/vnd.ernesto.hateoas+json":
                    return TypedResults.Ok(result.LinkedResource);
                default:
                    return TypedResults.Ok(result.ShapedOrder);
            }
        }
        public static async Task<Results<BadRequest, UnprocessableEntity<List<ValidationResult>>, CreatedAtRoute<ExpandoObject>, CreatedAtRoute<OrderDto>, Ok<OrderDto>>> CreateOrderAsync(
          [FromServices] ILibraryApplicationService _libraryApplicationService,
          [FromBody] OrderForCreationDto order
      )
        {
            if (order == null)
            {
                return TypedResults.BadRequest();
            }

            var result = await _libraryApplicationService.CreateOrderAsync(order);
            if (!result.Success)
            {
                return TypedResults.UnprocessableEntity(result.ValidationErrors);
            }

            //return TypedResults.CreatedAtRoute(result.Order, $"GetOrder", new { result.Order.OrderId });

            Guid orderId = (Guid)(result.ShapedOrder as IDictionary<string, object>)["OrderId"];

            return TypedResults.CreatedAtRoute(result.ShapedOrder, $"GetOrder", new { orderId });
        }
        public static async Task<Results<BadRequest, UnprocessableEntity<List<ValidationResult>>, CreatedAtRoute<ExpandoObject>, CreatedAtRoute<OrderDto>, Ok<OrderDto>>> CreateOrderWithDateofOrderAsync(
            [FromServices] ILibraryApplicationService _libraryApplicationService,
            [FromBody] OrderForCreationWithDateofOrderDto order
        )
        {
            if (order == null)
            {
                return TypedResults.BadRequest();
            }

            var result = await _libraryApplicationService.CreateOrderWithDateofOrderAsync(order);
            if (!result.Success)
            {
                return TypedResults.UnprocessableEntity(result.ValidationErrors);
            }
            Guid orderId = (Guid)(result.ShapedOrder as IDictionary<string, object>)["OrderId"];

            return TypedResults.CreatedAtRoute(result.ShapedOrder, $"GetOrder", new { orderId });

        }

        public static async Task<Results<NotFound, NoContent>> DeleteOrderAsync(
           [FromServices] ILibraryApplicationService _orderApplicationService,
           Guid orderId
        )
        {
            var result = await _orderApplicationService.DeleteOrderAsync(orderId);

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.NoContent();
        }


        public static async Task<Results<BadRequest, NotFound, NoContent, CreatedAtRoute<OrderDto> , UnprocessableEntity<List<ValidationResult>>, Ok<OrderDto>>> UpdateOrderAsync(
           [FromServices] ILibraryApplicationService _orderApplicationService,
           [FromBody] OrderForUpdateDto order,
           Guid orderId
        )
        {
            if (order == null)
            {
                return TypedResults.BadRequest();
            }

            var result = await _orderApplicationService.UpdateOrderAsync(orderId, order);

            if (result.Success && result.OrderUpserted != null)
            {
                return TypedResults.CreatedAtRoute(result.OrderUpserted, $"GetOrder", new { orderId = result.OrderUpserted.OrderId });
            }

            return TypedResults.NoContent();
        }
    }
}
