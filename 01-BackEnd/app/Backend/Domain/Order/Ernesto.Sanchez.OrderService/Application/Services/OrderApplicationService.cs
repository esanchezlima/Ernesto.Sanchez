using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ernesto.Sanchez.OrderService.Application.Interfaces;
using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Orders;
using System.ComponentModel.DataAnnotations;
using Ernesto.Sanchez.OrderService.Infrastructure.Results.Orders;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Extensions;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;

namespace Ernesto.Sanchez.OrderService.Application.Services
{
    public partial class LibraryApplicationService : ILibraryApplicationService
    {
        public async Task<GetOrdersResult> GetOrdersAsync(OrdersResourceParameters ordersResourceParameters)
        {
            GetOrdersResult result = new();
            _validationService.ValidateOrdersResourceParameters(ordersResourceParameters);
            var ordersFromRepo = await _unitOfWork.Orders.GetOrdersAsync(ordersResourceParameters);
            var orders = _mapper.Map<IEnumerable<OrderDto>>(ordersFromRepo);
            result.ShapedOrders = orders.ShapeData(ordersResourceParameters.Fields);
            result.PaginationMetadata = _orderLinksBuilder.GetPaginationMetadata(ordersFromRepo, ordersResourceParameters);

            var links = _orderLinksBuilder.CreatePagedLinksForOrders(ordersResourceParameters, ordersFromRepo.HasNext, ordersFromRepo.HasPrevious);
            var shapedOrdersWithLinks = _orderLinksBuilder.CreateDocumentationLinksForOrderShapeData(result.ShapedOrders, ordersResourceParameters);
            result.LinkedCollectionResource = new LinkedCollectionResource { Value = shapedOrdersWithLinks, Links = links };

            return result;
        }
        public async Task<GetOrderByOrderIdResult> GetOrderByOrderIdAsync(Guid orderId, string fields)
        {
            GetOrderByOrderIdResult result = new();
            var orderFromRepo = await _unitOfWork.Orders.GetOrderAsync(orderId);

            if (orderFromRepo == null)
            {
                return null;
            }

            var order = _mapper.Map<OrderDto>(orderFromRepo);
            result.ShapedOrder = order.ShapeData(fields);

            var links = _orderLinksBuilder.CreateDocumentationLinksForOrder(orderId, fields);
            result.LinkedResource = new Dictionary<string, object>(result.ShapedOrder);
            result.LinkedResource.Add("links", links);
            return result;
        }
        public async Task<CreateOrderResult> CreateOrderAsync(OrderForCreationDto order)
        {
            CreateOrderResult result = new();
            var validationResult = _validationService.ValidateOrderCreation(order);
            if (!validationResult.IsValid)
            {
                result.Success = false;
                result.ValidationErrors.AddRange(validationResult.Errors.Select(e => new ValidationResult(e.ErrorMessage)));
                return result;
            }

            var orderEntity = _mapper.Map<Order>(order);
            await _unitOfWork.Orders.AddOrderAsync(orderEntity);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Creating an order failed on save.");
            }
            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);
            result.ShapedOrder = orderToReturn.ShapeData(null);
            result.Success = true;

            var links = _orderLinksBuilder.CreateDocumentationLinksForOrder(orderToReturn.OrderId, null);
            result.LinkedResource = new Dictionary<string, object>(result.ShapedOrder);
            result.LinkedResource.Add("links", links);

            return result;

        }
 
        public async Task<CreateOrderWithDateofOrderhResult> CreateOrderWithDateofOrderAsync(OrderForCreationWithDateofOrderDto order)
        {
            CreateOrderWithDateofOrderhResult result = new();
            var validationResult = _validationService.ValidateOrderCreationWithDateofOrder(order);
            if (!validationResult.IsValid)
            {
                result.Success = false;
                result.ValidationErrors.AddRange(validationResult.Errors.Select(e => new ValidationResult(e.ErrorMessage)));
                return result;
            }

            var orderEntity = _mapper.Map<Order>(order);
            await _unitOfWork.Orders.AddOrderAsync(orderEntity);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Creating an order failed on save");
            }

            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);
            result.ShapedOrder = orderToReturn.ShapeData(null);

            var links = _orderLinksBuilder.CreateDocumentationLinksForOrder(orderToReturn.OrderId, null);
            result.LinkedResource = new Dictionary<string, object>(result.ShapedOrder);
            result.LinkedResource.Add("links", links);

            return result;
        }
        public async Task<bool?> DeleteOrderAsync(Guid orderId)
        {
            var orderFronRepo = await _unitOfWork.Orders.GetOrderAsync(orderId);
            if (orderFronRepo == null)
            {
                return null;
            }

            await _unitOfWork.Orders.DeleteOrderAsync(orderFronRepo);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Deleting order {orderId} failed on save.");
            }

            return true;
        }
        public async Task<UpdateOrderResult> UpdateOrderAsync(Guid orderId, OrderForUpdateDto order)
        {
            UpdateOrderResult result = new();
            var validationResult = _validationService.ValidateOrderUpdate(order);
            if (!validationResult.IsValid)
            {
                result.Success = false;
                result.ValidationErrors.AddRange(validationResult.Errors.Select(e => new ValidationResult(e.ErrorMessage)));
                return result;
            }

            var orderFromRepo = await _unitOfWork.Orders.GetOrderAsync(orderId);
            if (orderFromRepo == null)
            {
                var orderToAdd = _mapper.Map<Order>(order);
                orderToAdd.OrderId = orderId;

                await _unitOfWork.Orders.AddOrderAsync(orderToAdd);

                if (!await _unitOfWork.SaveAsync())
                {
                    throw new Exception($"Upserting order {orderId} failed on save.");
                }

                result.OrderUpserted = _mapper.Map<OrderDto>(orderToAdd);
                result.Success = true;

                return result;
            }

            _mapper.Map(order, orderFromRepo);
            await _unitOfWork.Orders.UpdateOrderAsync(orderFromRepo);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Updating order {orderId} failed on save.");
            }

            result.Success = true;
            return result;
        }
        public async Task<bool> OrderExistsAsync(Guid orderId)
        {
            return await _unitOfWork.Orders.OrderExists(orderId);
        }
    }
}









//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Ernesto.Sanchez.OrderService.Application.Interfaces;
//using Ernesto.Sanchez.OrderService.Application.Dtos;
//using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
//using Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Orders;

//namespace Ernesto.Sanchez.OrderService.Application.Services
//{
//    public partial class LibraryApplicationService : IOrderApplicationService
//    {   
//        public async Task<List<OrderDto>> GetOrdersAsync()
//        {            
//            var OrdersFromRepo = await _unitOfWork.Orders.GetOrdersAsync();
//            var Orders = _mapper.Map<List<OrderDto>>(OrdersFromRepo);

//            return Orders;
//        }
//         public async Task<OrderDto> GetOrderByOrderIdAsync(Guid orderId)
//        {            
//            var orderFromRepo = await _unitOfWork.Orders.GetOrderAsync(orderId);

//            if (orderFromRepo == null)
//            {
//                return null;
//            }

//            var order = _mapper.Map<OrderDto>(orderFromRepo);            
//            return order;
//        }


//        public async Task<OrderDto> CreateOrderAsync(OrderForCreationDto order)
//        {
//            var orderEntity = _mapper.Map<Order>(order);

//            await _unitOfWork.Orders.AddOrderAsync(orderEntity);

//            if (!await _unitOfWork.SaveAsync())
//            {
//                throw new Exception("Creating an Order failed on save.");
//            }

//            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);
//            return orderToReturn;
//        }

//        public async Task<OrderDto> CreateOrderWithDateofOrderAsync(OrderForCreationWithDateofOrderDto order)
//        {
//            var orderEntity = _mapper.Map<Order>(order);

//            await _unitOfWork.Orders.AddOrderAsync(orderEntity);

//            if (!await _unitOfWork.SaveAsync())
//            {
//                throw new Exception("Creating an Order failed on save");
//            }

//            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);
//            return orderToReturn;
//        }

//        public async Task<bool?> DeleteOrderAsync(Guid orderId)
//        {
//            var orderFronRepo = await _unitOfWork.Orders.GetOrderAsync(orderId);
//            if (orderFronRepo == null)
//            {
//                return null;
//            }

//            await _unitOfWork.Orders.DeleteOrderAsync(orderFronRepo);

//            if (!await _unitOfWork.SaveAsync())
//            {
//                throw new Exception($"Deleting Order {orderId} failed on save.");
//            }

//            return true;
//        }


//      public async Task<UpdateOrderResult> UpdateOrderAsync(Guid orderId, OrderForUpdateDto order)
//        {
//            UpdateOrderResult result = new();
//            var orderFromRepo = await _unitOfWork.Orders.GetOrderAsync(orderId);

//            if (orderFromRepo == null)
//            {
//                var orderToAdd = _mapper.Map<Order>(order);
//                orderToAdd.OrderId =orderId;

//                await _unitOfWork.Orders.AddOrderAsync(orderToAdd);

//                if (!await _unitOfWork.SaveAsync())
//                {
//                    throw new Exception($"Upserting order {orderId} failed on save.");
//                }

//                result.OrderUpserted = _mapper.Map<OrderDto>(orderToAdd);
//                result.Success = true;

//                return result;
//            }

//            _mapper.Map(order, orderFromRepo);
//            await _unitOfWork.Orders.UpdateOrderAsync(orderFromRepo);
//            if (!await _unitOfWork.SaveAsync())
//            {
//                throw new Exception($"Updating Order {orderId} failed on save.");
//            }

//            result.Success = true;
//            return result;
//        }
//        public async Task<bool> OrderExistsAsync(Guid orderId)
//        {
//            return await _unitOfWork.Orders.OrderExists(orderId);
//        }



//    }
//}
