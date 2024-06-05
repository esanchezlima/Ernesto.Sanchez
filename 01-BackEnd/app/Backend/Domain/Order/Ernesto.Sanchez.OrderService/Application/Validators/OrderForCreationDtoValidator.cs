using FluentValidation;
using Ernesto.Sanchez.OrderService.Application.Dtos;

namespace Ernesto.Sanchez.OrderService.Application.Validators
{
    public class OrderForCreationDtoValidator : AbstractValidator<OrderForCreationDto>
    { 
        public OrderForCreationDtoValidator()
        {
            RuleFor(x => x.Client).NotEmpty();
            RuleFor(x => x.AdressClient).NotEmpty();
            RuleFor(x => x.District).NotEmpty();
        }
    }

}
