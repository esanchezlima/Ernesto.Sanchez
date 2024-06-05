using FluentValidation.Results;
using Ernesto.Sanchez.OrderService.Application.Dtos;

namespace Ernesto.Sanchez.OrderService.Application.Interfaces
{
    public interface IValidationService
    {
        ValidationResult ValidateOrderCreation(OrderForCreationDto dto);
        ValidationResult ValidateOrderCreationWithDateofOrder(OrderForCreationWithDateofOrderDto dto);
        ValidationResult ValidateOrderUpdate(OrderForUpdateDto dto);
        ValidationResult ValidateOrdersResourceParameters(OrdersResourceParameters resource);
        ValidationResult ValidateItemCreation(ItemForCreationDto dto);
        ValidationResult ValidateItemUpdate(ItemForUpdateDto dto);

    }
}