using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;

namespace Ernesto.Sanchez.OrderService.Domain.Orders.Interfaces
{
    public interface IOrderDomainService
    {
        Task<Item> TransferItem(Guid sourceOrderId, Guid targetOrderId, Guid itemId);
    }
}