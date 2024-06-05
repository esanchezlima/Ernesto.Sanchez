using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.Paged;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders;

namespace Library.Service.Infrastructure.Http.Helpers.LinksBuilders
{
    public class OrderLinksBuilder : IOrderLinksBuilder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public OrderLinksBuilder(
            LinkGenerator linkGenerator,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public string CreateOrdersResourceUri(OrdersResourceParameters ordersResourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _linkGenerator.GetUriByName(_httpContextAccessor.HttpContext, "GetOrders",
                      new
                      {
                          fields = ordersResourceParameters.Fields,
                          orderBy = ordersResourceParameters.OrderBy,
                          searchQuery = ordersResourceParameters.SearchQuery,
                          district = ordersResourceParameters.District,
                          pageNumber = ordersResourceParameters.PageNumber - 1,
                          pageSize = ordersResourceParameters.PageSize
                      });
                case ResourceUriType.NextPage:
                    return _linkGenerator.GetUriByName(_httpContextAccessor.HttpContext, "GetOrders",
                      new
                      {
                          fields = ordersResourceParameters.Fields,
                          orderBy = ordersResourceParameters.OrderBy,
                          searchQuery = ordersResourceParameters.SearchQuery,
                          district = ordersResourceParameters.District,
                          pageNumber = ordersResourceParameters.PageNumber + 1,
                          pageSize = ordersResourceParameters.PageSize
                      });
                case ResourceUriType.Current:
                default:
                    return _linkGenerator.GetUriByName(_httpContextAccessor.HttpContext, "GetOrders",
                    new
                    {
                        fields = ordersResourceParameters.Fields,
                        orderBy = ordersResourceParameters.OrderBy,
                        searchQuery = ordersResourceParameters.SearchQuery,
                        district = ordersResourceParameters.District,
                        pageNumber = ordersResourceParameters.PageNumber,
                        pageSize = ordersResourceParameters.PageSize
                    });
            }
        }
        public PaginationMetadata GetPaginationMetadata(PagedList<Order> orders, OrdersResourceParameters ordersResourceParameters)
        {
            PaginationMetadata paginationMetadata = new()
            {
                PreviousPageLink = orders.HasPrevious ? CreateOrdersResourceUri(ordersResourceParameters, ResourceUriType.PreviousPage) : null,
                NextPageLink = orders.HasNext ? CreateOrdersResourceUri(ordersResourceParameters, ResourceUriType.NextPage) : null,
                TotalCount = orders.TotalCount,
                PageSize = orders.PageSize,
                CurrentPage = orders.CurrentPage,
                TotalPages = orders.TotalPages,
            };

            return paginationMetadata;
        }
        public IEnumerable<LinkDto> CreatePagedLinksForOrders(OrdersResourceParameters ordersResourceParameters, bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>
            {
                // self 
                new LinkDto(CreateOrdersResourceUri(ordersResourceParameters, ResourceUriType.Current), "self", "GET")
            };

            if (hasNext)
            {
                links.Add(new LinkDto(CreateOrdersResourceUri(ordersResourceParameters, ResourceUriType.NextPage), "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(new LinkDto(CreateOrdersResourceUri(ordersResourceParameters, ResourceUriType.PreviousPage), "previousPage", "GET"));
            }

            return links;
        }
        public IEnumerable<IDictionary<string, object>> CreateDocumentationLinksForOrderShapeData(IEnumerable<ExpandoObject> shapedOrders, OrdersResourceParameters ordersResourceParameters)
        {
            var shapedOrdersWithLinks = shapedOrders.Select(order =>
            {
                IDictionary<string, object> orderAsDictionary = new Dictionary<string, object>((order as IDictionary<string, object>));
                var orderLinks = CreateDocumentationLinksForOrder((Guid)orderAsDictionary["OrderId"], ordersResourceParameters.Fields);
                orderAsDictionary.Add("links", orderLinks);

                return orderAsDictionary;
            });

            return shapedOrdersWithLinks.ToList();
        }
        public List<LinkDto> CreateDocumentationLinksForOrder(Guid orderId, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(new LinkDto(_linkGenerator.GetUriByName(_httpContextAccessor.HttpContext, "GetOrder", new { orderId }), "self", "GET"));
            }
            else
            {
                links.Add(new LinkDto(_linkGenerator.GetUriByName(_httpContextAccessor.HttpContext, "GetOrder", new { orderId, fields }), "self", "GET"));
            }

            links.Add(new LinkDto(_linkGenerator.GetUriByName(_httpContextAccessor.HttpContext, "DeleteOrder", new { orderId }), "delete_order", "DELETE"));
            links.Add(new LinkDto(_linkGenerator.GetUriByName(_httpContextAccessor.HttpContext, "UpdateOrder", new { orderId }), "update_order", "PUT"));

            return links;
        }
    }
}
