using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.Paged;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Results.Orders
{
    public class GetOrdersResult
    {
        // public List<OrderDto> Orders { get; set; }
        public IEnumerable<ExpandoObject> ShapedOrders { get; set; }
        public PaginationMetadata PaginationMetadata { get; set; }
        public LinkedCollectionResource LinkedCollectionResource { get; set; }

    }
}
