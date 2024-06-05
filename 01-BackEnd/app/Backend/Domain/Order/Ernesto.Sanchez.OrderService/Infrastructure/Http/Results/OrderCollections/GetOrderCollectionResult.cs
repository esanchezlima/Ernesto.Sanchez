using Ernesto.Sanchez.OrderService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.OrderCollections
{
    public class GetOrderCollectionResult
    {
        public IEnumerable<OrderDto> OrderCollection { get; set; }
        public bool OrdersFound { get; set; }
    }
}
