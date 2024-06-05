using FluentValidation;
using FluentValidation.Results;
using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Application.Interfaces;

namespace Ernesto.Sanchez.OrderService.Application.Validators
{
    public class ValidationService : IValidationService
    {
        private readonly IValidator<OrderForCreationDto> _orderCreationValidator;
        private readonly IValidator<OrderForCreationWithDateofOrderDto> _orderCreationWithDateofOrderValidator;
        private readonly IValidator<OrderForUpdateDto> _orderUpdateValidator;
        private readonly IValidator<OrdersResourceParameters> _ordersResourceParametersValidator;

        private readonly IValidator<ItemForCreationDto> _itemCreationValidator;
        private readonly IValidator<ItemForUpdateDto> _itemUpdateValidator;
        public ValidationService(
            IValidator<OrderForCreationDto> orderCreationValidator,
            IValidator<OrderForUpdateDto> orderUpdateValidator,
            IValidator<OrderForCreationWithDateofOrderDto> orderCreationWithDateofOrderValidator,
            IValidator<OrdersResourceParameters> ordersResourceParametersValidator,
            IValidator<ItemForCreationDto> itemCreationValidator,
            IValidator<ItemForUpdateDto> itemUpdateValidator
        )
        {
            _orderCreationValidator = orderCreationValidator;
            _orderUpdateValidator = orderUpdateValidator;
            _orderCreationWithDateofOrderValidator = orderCreationWithDateofOrderValidator;
            _ordersResourceParametersValidator = ordersResourceParametersValidator;
            _itemCreationValidator = itemCreationValidator;
            _itemUpdateValidator = itemUpdateValidator;
        }
        public ValidationResult ValidateOrderCreation(OrderForCreationDto dto)
        {
            return _orderCreationValidator.Validate(dto);
        }
        public ValidationResult ValidateOrderCreationWithDateofOrder(OrderForCreationWithDateofOrderDto dto)
        {
            return _orderCreationWithDateofOrderValidator.Validate(dto);
        }
        public ValidationResult ValidateOrderUpdate(OrderForUpdateDto dto)
        {
            return _orderUpdateValidator.Validate(dto);
        }
        public ValidationResult ValidateOrdersResourceParameters(OrdersResourceParameters resource)
        {
            return _ordersResourceParametersValidator.Validate(resource);
        }

        public ValidationResult ValidateItemCreation(ItemForCreationDto dto)
        {
            return _itemCreationValidator.Validate(dto);
        }

        public ValidationResult ValidateItemUpdate(ItemForUpdateDto dto)
        {
            return _itemUpdateValidator.Validate(dto);
        }
    }
}