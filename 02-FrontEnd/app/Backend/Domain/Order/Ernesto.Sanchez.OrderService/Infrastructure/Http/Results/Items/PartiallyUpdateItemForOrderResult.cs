using Ernesto.Sanchez.OrderService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Items
{
    public class PartiallyUpdateItemForOrderResult
    {
        public ItemDto ItemUpserted { get; set; } = new();
        public bool Success { get; set; }
        public List<ValidationResult> ValidationErrors { get; set; } = new();
        
    }
}
