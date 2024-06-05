using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.DataMapping.PropertyMapping;


namespace Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.DataMapping.ModelMapping
{
    public interface IOrderPropertyMappingService : IPropertyMappingService<OrderDto, Order>
    {

    }
}
