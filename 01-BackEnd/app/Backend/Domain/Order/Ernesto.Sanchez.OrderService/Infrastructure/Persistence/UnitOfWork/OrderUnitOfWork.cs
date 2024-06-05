using Ernesto.Sanchez.OrderService.Domain.Orders.Interfaces;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Persistence.UnitOfWork
{
    public class OrderUnitOfWork: UnitOfWork
    {
        public IOrderRepository Orders { get; }
        public IItemRepository Items { get; }
        public OrderContext _context { get; }

        public OrderUnitOfWork(
            OrderContext context,
            IOrderRepository orderRepository,
            IItemRepository itemRepository
        ) :base(context)
        {
            _context = context;
            Orders = orderRepository;
            Items = itemRepository;
        }
    }
}
