using Ernesto.Sanchez.OrderService.Domain.Orders.Interfaces;
using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;

namespace Ernesto.Sanchez.OrderService.Domain.Orders.Services
{
    public class OrderDomainService : IOrderDomainService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;

        public OrderDomainService(IOrderRepository orderRepository, IItemRepository itemRepository)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
        }

        public async Task<Item> TransferItem(Guid sourceOrderId, Guid targetOrderId, Guid itemId)
        {
            var sourceOrder = await _orderRepository.GetOrderAsync(sourceOrderId);
            var targetOrder = await _orderRepository.GetOrderAsync(targetOrderId);
            var item = await _itemRepository.GetItemForOrderAsync(sourceOrderId, itemId);

            if (sourceOrder == null || targetOrder == null || item == null)
            {
                throw new Exception("Invalid operation");
            }

            item.OrderId = targetOrderId;

            return await _itemRepository.UpdateItemAsync(item);
        }
    }
}
