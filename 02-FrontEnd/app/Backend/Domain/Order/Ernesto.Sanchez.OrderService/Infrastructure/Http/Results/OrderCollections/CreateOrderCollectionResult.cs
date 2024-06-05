using Ernesto.Sanchez.OrderService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.OrderCollections
{
    public class CreateOrderCollectionResult
    {
        public IEnumerable<OrderDto> OrdersCollection { get; set; }
        public string OrdersIdsAsString { get; set; }
    }
}
