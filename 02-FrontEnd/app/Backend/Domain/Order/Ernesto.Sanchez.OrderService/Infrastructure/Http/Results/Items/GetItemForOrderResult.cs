using Ernesto.Sanchez.OrderService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Items
{
    public class GetItemForOrderResult
    {
        public ItemDto LinkedResource { get; set; }
    }
}
