using AutoMapper;
using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ernesto.Sanchez.OrderService.Application.Dtos;

namespace Ernesto.Sanchez.OrderService.Application.Mappers
{
    public class ItemMapper : Profile
    {
        public ItemMapper()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<ItemForCreationDto, Item>();
            CreateMap<ItemForUpdateDto, Item>();
            CreateMap<Item, ItemForUpdateDto>();
        }
    }
}
