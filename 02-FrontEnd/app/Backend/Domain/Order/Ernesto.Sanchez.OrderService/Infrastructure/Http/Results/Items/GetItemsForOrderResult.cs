using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Items
{
    public class GetItemsForOrderResult
    {
        public LinkedCollectionResourceWrapperDto<ItemDto> LinkedCollectionResource { get; set; }
    }
}
