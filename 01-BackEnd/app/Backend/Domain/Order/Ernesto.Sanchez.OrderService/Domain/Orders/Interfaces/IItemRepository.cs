using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;

namespace Ernesto.Sanchez.OrderService.Domain.Orders.Interfaces
{
    public interface IItemRepository
    {
        Task AddItemForOrderAsync(Guid orderId, Item item);
        Task DeleteItemAsync(Item item);
        Task<Item> GetItemForOrderAsync(Guid orderId, Guid itemId);
        Task<IEnumerable<Item>> GetItemsForOrderAsync(Guid orderId);
        Task<Item> UpdateItemAsync(Item item);
        Task UpdateItemForOrderAsync(Item item);
    }
}