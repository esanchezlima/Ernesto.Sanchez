using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Application.Interfaces;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.EndpointHandlers
{
    public static class ItemsHandlers
    {
        public static async Task<Results<NotFound,Ok<LinkedCollectionResourceWrapperDto<ItemDto>>>> GetItemsForOrderAsync(
            [FromServices] ILibraryApplicationService _libraryApplicationService,
            Guid orderId
        )
        {
            if (!await _libraryApplicationService.OrderExistsAsync(orderId))
            {
                return TypedResults.NotFound();
            }

            var result = await _libraryApplicationService.GetItemsForOrderAsync(orderId);

            return TypedResults.Ok(result.LinkedCollectionResource);
        }
        public static async Task<Results<NotFound,Ok<ItemDto>>> GetItemByItemIdForOrderAsync(
            [FromServices] ILibraryApplicationService _libraryApplicationService,
            Guid orderId,
            Guid itemId
        )
        {
            if (!await _libraryApplicationService.OrderExistsAsync(orderId))
            {
                return TypedResults.NotFound();
            }

            var result = await _libraryApplicationService.GetItemByItemIdForOrderAsync(orderId, itemId);
            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result.LinkedResource);
        }
        public static async Task<Results<BadRequest, NotFound, UnprocessableEntity<List<ValidationResult>>, CreatedAtRoute<ItemDto>>> CreateItemForOrderAsync(
            [FromServices] ILibraryApplicationService _libraryApplicationService,
            Guid orderId,
            [FromBody] ItemForCreationDto item
        )
        {
            if (item == null)
            {
                return TypedResults.BadRequest();
            }

            if (! await _libraryApplicationService.OrderExistsAsync(orderId))
            {
                return TypedResults.NotFound();
            }

            var result = await _libraryApplicationService.CreateItemForOrderAsync(orderId, item);

            if (!result.Success)
            {                
                return TypedResults.UnprocessableEntity(result.ValidationErrors);  
            }

            return TypedResults.CreatedAtRoute(result.LinkedResource, $"GetItemForOrder", new { orderId , itemId = result.LinkedResource.ItemId });            
        }
        public static async Task<Results<NotFound, NoContent>> DeleteItemForOrderAsync(
            [FromServices] ILibraryApplicationService _libraryApplicationService,
            Guid orderId,
            Guid itemId
        )
        {
            var result = await _libraryApplicationService.DeleteItemForOrderAsync(orderId, itemId);

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.NoContent();
        }
        public static async Task<Results<BadRequest, NotFound, NoContent, CreatedAtRoute<ItemDto>, UnprocessableEntity<List<ValidationResult>>>> UpdateItemForOrderAsync(
            [FromServices] ILibraryApplicationService _libraryApplicationService,
            [FromBody] ItemForUpdateDto item,
            Guid orderId,
            Guid itemId
        )
        {
            if (item == null)
            {
                return TypedResults.BadRequest();
            }

            if (!await _libraryApplicationService.OrderExistsAsync(orderId))
            {
                return TypedResults.NotFound();
            }

            var result = await _libraryApplicationService.UpdateItemForOrderAsync(orderId, itemId, item);

            if (!result.Success)
            {
                return TypedResults.UnprocessableEntity(result.ValidationErrors);
            }
            else
            {
                if (result.Success && result.ItemUpserted != null)
                {
                    return TypedResults.CreatedAtRoute(result.ItemUpserted, "UpdateItemForOrder", new { orderId, itemId = result.ItemUpserted.ItemId });
                }
            }

            return TypedResults.NoContent();
        }

        public static async Task<Results<BadRequest, NotFound, NoContent, CreatedAtRoute<ItemDto>, UnprocessableEntity<List<ValidationResult>>>> PartiallyUpdateItemForOrderAsync(
            [FromServices] ILibraryApplicationService _libraryApplicationService,
            [FromBody] JsonPatchDocument<ItemForUpdateDto> patchDoc,
            Guid orderId,
            Guid itemId
        )
        {
            if (patchDoc == null)
            {
                return TypedResults.BadRequest();
            }

            if (!await _libraryApplicationService.OrderExistsAsync(orderId))
            {
                return TypedResults.NotFound();
            }

            var result = await _libraryApplicationService.PartiallyUpdateItemForOrder(orderId, itemId, patchDoc);

            if (!result.Success)
            {
                return TypedResults.UnprocessableEntity(result.ValidationErrors);
            }
            else
            {
                if (result.ItemUpserted != null)
                {
                    return TypedResults.CreatedAtRoute(result.ItemUpserted, "GetItemForOrder", new { orderId, itemId = result.ItemUpserted.ItemId });
                }
            }

            return TypedResults.NoContent();
        }
    }
}
