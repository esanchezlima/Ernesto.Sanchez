using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using System.Dynamic;
using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.Paged;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders
{
    public interface IOrderLinksBuilder
    {
        string CreateOrdersResourceUri(OrdersResourceParameters ordersResourceParameters, ResourceUriType type);        
        PaginationMetadata GetPaginationMetadata(PagedList<Order> orders, OrdersResourceParameters ordersResourceParameters);
        List<LinkDto> CreateDocumentationLinksForOrder(Guid orderId, string fields);
        IEnumerable<IDictionary<string, object>> CreateDocumentationLinksForOrderShapeData(IEnumerable<ExpandoObject> shapedOrders, OrdersResourceParameters ordersResourceParameters);
        IEnumerable<LinkDto> CreatePagedLinksForOrders(OrdersResourceParameters ordersResourceParameters, bool hasNext, bool hasPrevious);
    }
}