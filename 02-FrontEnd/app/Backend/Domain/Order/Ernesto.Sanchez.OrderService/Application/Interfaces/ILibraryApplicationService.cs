using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.OrderCollections;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Orders;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Items;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Root;
using Ernesto.Sanchez.OrderService.Infrastructure.Results.Orders;
using Microsoft.AspNetCore.JsonPatch;


namespace Ernesto.Sanchez.OrderService.Application.Interfaces
{
    public interface ILibraryApplicationService
    {
        Task<GetOrdersResult> GetOrdersAsync(OrdersResourceParameters ordersResourceParameters);
        Task<bool> OrderExistsAsync(Guid orderId);
        Task<CreateOrderResult> CreateOrderAsync(OrderForCreationDto order);
        Task<CreateOrderWithDateofOrderhResult> CreateOrderWithDateofOrderAsync(OrderForCreationWithDateofOrderDto order);
        Task<bool?> DeleteOrderAsync(Guid orderId);
        Task<GetOrderByOrderIdResult> GetOrderByOrderIdAsync(Guid orderId, string fields);
        Task<UpdateOrderResult> UpdateOrderAsync(Guid orderId, OrderForUpdateDto order);

        Task<CreateItemForOrderResult> CreateItemForOrderAsync(Guid orderId, ItemForCreationDto item);
        Task<DeleteItemForOrderResult> DeleteItemForOrderAsync(Guid orderId, Guid itemId);
        Task<GetOrderCollectionResult> GetOrdersCollectionAsync(List<Guid> ordersIds);
        Task<GetItemForOrderResult> GetItemByItemIdForOrderAsync(Guid orderId, Guid itemId);
        Task<GetItemsForOrderResult> GetItemsForOrderAsync(Guid orderId);
        Task<PartiallyUpdateItemForOrderResult> PartiallyUpdateItemForOrder(Guid orderId, Guid itemId, JsonPatchDocument<ItemForUpdateDto> patchDoc);
        Task<UpdateItemForOrderResult> UpdateItemForOrderAsync(Guid orderId, Guid itemId, ItemForUpdateDto itemDto);

        Task<CreateOrderCollectionResult> CreateOrderCollectionAsync(IEnumerable<OrderForCreationDto> orderCollection);
        Task<GetRootResult> GetRootAsync();

    }
}