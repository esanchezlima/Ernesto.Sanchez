﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Ernesto.Sanchez.OrderService.Domain.Orders.Entities;

public partial class Item
{
    public Guid ItemId { get; set; }

    public Guid OrderId { get; set; }

    public string DescriptionProduct { get; set; }

    public int Cant { get; set; }

    public virtual Order Order { get; set; }
}