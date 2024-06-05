using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.Paged;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.Paged;
namespace Ernesto.Sanchez.OrderService.Domain.Orders.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<bool> OrderExists(Guid orderId);
        Task DeleteOrderAsync(Order order);
        Task<Order> GetOrderAsync(Guid orderId);
        Task<PagedList<Order>> GetOrdersAsync(OrdersResourceParameters ordersResourceParameters);
       // Task<List<Order>> GetOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersAsync(List<Guid> orderIds);
        Task UpdateOrderAsync(Order order);
    }
}