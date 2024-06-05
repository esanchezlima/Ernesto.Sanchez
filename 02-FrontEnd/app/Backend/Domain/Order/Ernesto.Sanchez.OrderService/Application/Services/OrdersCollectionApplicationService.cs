using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Application.Interfaces;
using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.OrderCollections;

namespace Ernesto.Sanchez.OrderService.Application.Services
{
    public partial class LibraryApplicationService : ILibraryApplicationService
    {        
        public async Task<GetOrderCollectionResult> GetOrdersCollectionAsync(List<Guid> ordersIds)
        {
            GetOrderCollectionResult result = new GetOrderCollectionResult();
            var ordersEntities = await _unitOfWork.Orders.GetOrdersAsync(ordersIds);

            if (!ordersEntities.Any())
            {
                result.OrdersFound = false;
                return result;
            }

            result.OrdersFound = true;
            result.OrderCollection = _mapper.Map<IEnumerable<OrderDto>>(ordersEntities);

            return result;
        }
        public async Task<CreateOrderCollectionResult> CreateOrderCollectionAsync(IEnumerable<OrderForCreationDto> orderCollection)
        {
            CreateOrderCollectionResult result = new CreateOrderCollectionResult();
            var orderEntities = _mapper.Map<IEnumerable<Order>>(orderCollection);

            foreach (var order in orderEntities)
            {
                await _unitOfWork.Orders.AddOrderAsync(order);
            }

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Creating an order collection failed on save.");
            }

            result.OrdersCollection = _mapper.Map<IEnumerable<OrderDto>>(orderEntities);
            result.OrdersIdsAsString = string.Join(",", result.OrdersCollection.Select(a => a.OrderId));

            return result;
        }
    }
}
