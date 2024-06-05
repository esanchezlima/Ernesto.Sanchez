using FluentValidation;
using Ernesto.Sanchez.OrderService.Application.Dtos;

namespace Ernesto.Sanchez.OrderService.Application.Validators
{
    public class ItemForCreationDtoValidator : ItemForManipulationDtoValidator<ItemForCreationDto>
    { 
        public ItemForCreationDtoValidator()
        {
            RuleFor(x => x.Cant).NotEmpty();
           //RuleFor(x => x.DescriptionProduct).NotEmpty().NotEqual(x => x.Title)
           //     .WithMessage("The provided description should be different from the title.");            
        }
    }

}
