using FluentValidation;
using Ernesto.Sanchez.OrderService.Application.Dtos;

namespace Ernesto.Sanchez.OrderService.Application.Validators
{
    public class ItemForManipulationDtoValidator<T> : AbstractValidator<T> where T : ItemForManipulationDto
    {
        public ItemForManipulationDtoValidator()
        {            
        }
    }
}
