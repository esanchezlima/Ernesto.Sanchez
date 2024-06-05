using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Application.Dtos
{
    public class ItemDto : LinkedResourceBaseDto
    {
        public Guid ItemId { get; set; }
        public Int16 Cant { get; set; }
        public string DescriptionProduct { get; set; }
        public Guid OrderId { get; set; }
    }
}
