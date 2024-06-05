using FluentValidation;
using Ernesto.Sanchez.OrderService.Application.Dtos;

namespace Ernesto.Sanchez.OrderService.Application.Validators
{
    public class ItemForUpdateDtoValidator : ItemForManipulationDtoValidator<ItemForUpdateDto>
    {
        public ItemForUpdateDtoValidator()
        {
            RuleFor(x => x.Cant).NotEmpty();
            //RuleFor(x => x.Description).NotEmpty().NotEqual(x => x.Title)
            //    .WithMessage("The provided description should be different from the title.");            
        }
    }

}
