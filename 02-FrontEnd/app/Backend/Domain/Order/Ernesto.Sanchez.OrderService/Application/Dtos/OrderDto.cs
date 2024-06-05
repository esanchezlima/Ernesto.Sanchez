using System;

namespace Ernesto.Sanchez.OrderService.Application.Dtos
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string Client { get; set; }
        public string AdressClient { get; set; }
        public string District { get; set; }
        public string AdressComplete { get; set; }
        public DateTimeOffset DateofOrder{ get; set; }
        public int DaysOrder { get; set; }

    }
}
