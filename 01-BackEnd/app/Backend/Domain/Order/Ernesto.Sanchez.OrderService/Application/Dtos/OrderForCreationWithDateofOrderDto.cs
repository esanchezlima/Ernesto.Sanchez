using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Application.Dtos
{
    public class OrderForCreationWithDateofOrderDto
    {
        public string Client { get; set; }
        public string AdressClient { get; set; }
        public DateTimeOffset DateofOrder { get; set; }
        public string District { get; set; }
    }
}
