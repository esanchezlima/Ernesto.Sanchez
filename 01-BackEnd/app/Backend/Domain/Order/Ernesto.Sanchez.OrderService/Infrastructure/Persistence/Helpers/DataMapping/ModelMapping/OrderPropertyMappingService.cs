using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.DataMapping.PropertyMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.DataMapping.ModelMapping
{
    public class OrderPropertyMappingService : PropertyMappingService<OrderDto, Order>, IOrderPropertyMappingService
    {
        private static Dictionary<string, PropertyMappingValue> _orderPropertyMapping =
           new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
           {
               { "Id", new PropertyMappingValue(new List<string>() { "OrderId" } ) },
               { "Client", new PropertyMappingValue(new List<string>() { "Client" } )},
               { "DateofOrder", new PropertyMappingValue(new List<string>() { "DateofOrder" } , true) },
               { "AdressComplete", new PropertyMappingValue(new List<string>() { "Adress", "District" }) },
               { "District", new PropertyMappingValue(new List<string>() {  "District" }) }
           };
        public OrderPropertyMappingService() : base(_orderPropertyMapping)
        {

        }
    }
}
