using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Application.Dtos
{
    public abstract class ItemForManipulationDto
    {
        public Int16 Cant { get; set; }
        public string? DescriptionProduct { get; set; }
    }
}
