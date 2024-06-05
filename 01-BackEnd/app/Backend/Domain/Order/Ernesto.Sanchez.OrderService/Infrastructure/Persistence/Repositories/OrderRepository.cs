using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using Ernesto.Sanchez.OrderService.Domain.Orders.Interfaces;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Contexts;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Extensions;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.DataMapping.ModelMapping;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.Paged;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;
        private readonly IOrderPropertyMappingService _orderPropertyMappingService;
        public OrderRepository(
            OrderContext context,
            IOrderPropertyMappingService propertyMappingService
        )
        {
            _context = context;
            _orderPropertyMappingService = propertyMappingService;
        }
        //public async Task<List<Order>> GetOrdersAsync()
        //{
        //    var orders = _context.Orders.ToList();
        //    return orders;
        //}

        public async Task<PagedList<Order>> GetOrdersAsync(OrdersResourceParameters ordersResourceParameters)
        {
            //Ordenamiento
            var collectionBeforePaging = _context.Orders.ApplySort(ordersResourceParameters.OrderBy, _orderPropertyMappingService.GetPropertyMapping());

            //Defino si voy a flitrar por distrito
            if (!string.IsNullOrEmpty(ordersResourceParameters.District))
            {

                var genreForWhereClause = ordersResourceParameters.District.Trim().ToLower();
                collectionBeforePaging = collectionBeforePaging.Where(a => a.District.ToLower() == genreForWhereClause);
            }

            //Defino si filtro por busqueda

            if (!string.IsNullOrEmpty(ordersResourceParameters.SearchQuery))
            {
                var searchQueryForWhereClause = ordersResourceParameters.SearchQuery.Trim().ToLower();

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Client.ToLower().Contains(searchQueryForWhereClause)
                    || a.AdressClient.ToLower().Contains(searchQueryForWhereClause)
                    || a.District.ToLower().Contains(searchQueryForWhereClause));
            }

            //return queryOrders.ToList();
            //Consulta de Paginado
            return await PagedList<Order>.CreateAsync(collectionBeforePaging,
               ordersResourceParameters.PageNumber.Value,
               ordersResourceParameters.PageSize.Value);

        }


        public async Task<IEnumerable<Order>> GetOrdersAsync(List<Guid> ordersIds)
        {
            return await _context.Orders.Where(a => ordersIds.Contains(a.OrderId))
                .OrderBy(a => a.Client)
                .OrderBy(a => a.District)
                .ToListAsync();
        }
        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }
        public async Task DeleteOrderAsync(Order order)
        {
            await Task.FromResult(_context.Orders.Remove(order));
        }
        public async Task UpdateOrderAsync(Order order)
        {
            var orderUpdate = await GetOrderAsync(order.OrderId);
            if (orderUpdate != null)
            {
                orderUpdate.Client = order.Client;
                orderUpdate.AdressClient = order.AdressClient;
                orderUpdate.District = order.District;
                orderUpdate.DateofOrder = order.DateofOrder; 
            }
        }
        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(a => a.OrderId == orderId);
        }
        public async Task<bool> OrderExists(Guid orderId)
        {
            return await _context.Orders.AnyAsync(a => a.OrderId == orderId);
        }
    }
}
