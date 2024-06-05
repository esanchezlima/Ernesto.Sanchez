using Ernesto.Sanchez.OrderService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Orders
{
    public class GetOrderByOrderIdResult
    {
        public ExpandoObject ShapedOrder { get; set; }

        public IDictionary<string, object> LinkedResource { get; set; }
    }
}
