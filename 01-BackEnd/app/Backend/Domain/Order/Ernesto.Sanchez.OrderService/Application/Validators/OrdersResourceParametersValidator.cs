using FluentValidation;
using Ernesto.Sanchez.OrderService.Application.Dtos;

namespace Ernesto.Sanchez.OrderService.Application.Validators
{
    public class OrdersResourceParametersValidator: AbstractValidator<OrdersResourceParameters>
    {
        public OrdersResourceParametersValidator()
        {
            RuleFor(x => x.Fields).Custom((fields, context) =>
            {
                if (!string.IsNullOrEmpty(fields))
                {
                    var fieldList = fields.Split(',');
                    if (!fieldList.Any(f => f.Trim().Equals("OrderId", StringComparison.OrdinalIgnoreCase)))
                    {
                        context.InstanceToValidate.Fields = fields + ",OrderId";
                    }
                }
            });
        }
    }
}
