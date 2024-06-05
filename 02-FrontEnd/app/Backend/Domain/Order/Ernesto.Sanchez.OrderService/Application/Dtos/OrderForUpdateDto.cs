using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Application.Dtos
{
    public class OrderForUpdateDto
    {
        public OrderForUpdateDto()
        {
        }
        public string Client { get; set; }
        public string AdressClient { get; set; }
        public string District { get; set; }
        public DateTimeOffset DateofOrder { get; set; }

    }
}
