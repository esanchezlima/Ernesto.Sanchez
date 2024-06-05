using AutoMapper;
using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Extensions;

namespace Ernesto.Sanchez.OrderService.Application.Mappers
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.AdressComplete, opt => opt.MapFrom(src => $"{src.AdressClient} {src.District}"));
            //.ForMember(dest => dest.DaysOrder, opt => opt.MapFrom(src => src.DateofOrder.GetCurrentDay(DateTime.UtcNow)));

            CreateMap<OrderForCreationDto, Order>();
            CreateMap<OrderForCreationWithDateofOrderDto, Order>();
            CreateMap<OrderForUpdateDto, Order>();
            CreateMap<Order, OrderForUpdateDto>();

        }
    }
}
