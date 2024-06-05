using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Application.Interfaces;
using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Items;
using Microsoft.AspNetCore.JsonPatch;
using System.ComponentModel.DataAnnotations;

namespace Ernesto.Sanchez.OrderService.Application.Services
{
    public partial class LibraryApplicationService : ILibraryApplicationService
    {        
        public async Task<GetItemsForOrderResult> GetItemsForOrderAsync(Guid orderId)
        {
            GetItemsForOrderResult result = new GetItemsForOrderResult();
            var itemsForOrderFromRepo = await _unitOfWork.Items.GetItemsForOrderAsync(orderId);
            var itemsForOrder = _mapper.Map<IEnumerable<ItemDto>>(itemsForOrderFromRepo);

            itemsForOrder = itemsForOrder.Select(item =>
            {
                item = _itemLinksBuilder.CreateDocumentationLinksForItem(item);
                return item;
            });

            var wrapper = new LinkedCollectionResourceWrapperDto<ItemDto>(itemsForOrder);
            result.LinkedCollectionResource = _itemLinksBuilder.CreateDocumentationLinksForItems(orderId, wrapper);

            return result;
        }
        public async Task<GetItemForOrderResult> GetItemByItemIdForOrderAsync(Guid orderId, Guid itemId)
        {
            GetItemForOrderResult result = new GetItemForOrderResult();
            var itemForOrderFromRepo = await _unitOfWork.Items.GetItemForOrderAsync(orderId, itemId);
            if (itemForOrderFromRepo == null)
            {
                return null;
            }

            var itemForOrder = _mapper.Map<ItemDto>(itemForOrderFromRepo);
            result.LinkedResource = _itemLinksBuilder.CreateDocumentationLinksForItem(itemForOrder);

            return result;
        }
        public async Task<CreateItemForOrderResult> CreateItemForOrderAsync(Guid orderId, ItemForCreationDto itemDto)
        {
            CreateItemForOrderResult result = new CreateItemForOrderResult();
            var itemEntity = _mapper.Map<Item>(itemDto);

            var validationResult = _validationService.ValidateItemCreation(itemDto);
            if (!validationResult.IsValid)
            {
                result.Success = false;
                result.ValidationErrors.AddRange(validationResult.Errors.Select(e => new ValidationResult(e.ErrorMessage)));
                return result;
            }

            await _unitOfWork.Items.AddItemForOrderAsync(orderId, itemEntity);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Creating a item for order {orderId} failed on save.");
            }

            var itemToReturn = _mapper.Map<ItemDto>(itemEntity);

            result.LinkedResource = _itemLinksBuilder.CreateDocumentationLinksForItem(itemToReturn);
            result.Success = true;

            return result;
        }

        public async Task<DeleteItemForOrderResult> DeleteItemForOrderAsync(Guid orderId, Guid itemId)
        {
            DeleteItemForOrderResult result = new DeleteItemForOrderResult();
            var itemForOrderFromRepo = await _unitOfWork.Items.GetItemForOrderAsync(orderId, itemId);
            if (itemForOrderFromRepo == null)
            {
                return null;
            }

            await _unitOfWork.Items.DeleteItemAsync(itemForOrderFromRepo);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Deleting item {itemId} for order {orderId} failed on save.");
            }

            result.Success = true;
            return result;
        }

        public async Task<UpdateItemForOrderResult> UpdateItemForOrderAsync(Guid orderId, Guid itemId, ItemForUpdateDto itemDto)
        {
            UpdateItemForOrderResult result = new UpdateItemForOrderResult();
            var itemForOrderFromRepo = await _unitOfWork.Items.GetItemForOrderAsync(orderId, itemId);

            if (itemForOrderFromRepo == null)
            {                
                var validationInsertResult = _validationService.ValidateItemUpdate(itemDto);

                if (!validationInsertResult.IsValid)
                {
                    result.Success = false;
                    result.ValidationErrors.AddRange(validationInsertResult.Errors.Select(e => new ValidationResult(e.ErrorMessage)));
                    return result;
                }

                var itemToAdd = _mapper.Map<Item>(itemDto);
                itemToAdd.ItemId = itemId;
                await _unitOfWork.Items.AddItemForOrderAsync(orderId, itemToAdd);

                if (!await _unitOfWork.SaveAsync())
                {
                    throw new Exception($"Upserting item {itemId} for order {orderId} failed on save.");
                }

                result.ItemUpserted = _itemLinksBuilder.CreateDocumentationLinksForItem(_mapper.Map<ItemDto>(itemToAdd));                
                result.Success = true;

                return result;
            }
                        
            var validationUpdateResult = _validationService.ValidateItemUpdate(itemDto);
            
            if (!validationUpdateResult.IsValid)
            {
                result.Success = false;
                result.ValidationErrors.AddRange(validationUpdateResult.Errors.Select(e => new ValidationResult(e.ErrorMessage)));
                return result;
            }

            _mapper.Map(itemDto, itemForOrderFromRepo);
            await _unitOfWork.Items.UpdateItemForOrderAsync(itemForOrderFromRepo);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Updating item {itemId} for order {orderId} failed on save.");
            }

            result.ItemUpserted = _itemLinksBuilder.CreateDocumentationLinksForItem(_mapper.Map<ItemDto>(itemForOrderFromRepo));
            result.Success = true;
            return result;
        }

        public async Task<PartiallyUpdateItemForOrderResult> PartiallyUpdateItemForOrder(Guid orderId, Guid itemId, JsonPatchDocument<ItemForUpdateDto> patchDoc)
        {
            PartiallyUpdateItemForOrderResult result = new PartiallyUpdateItemForOrderResult();
            var itemForOrderFromRepo = await _unitOfWork.Items.GetItemForOrderAsync(orderId, itemId);

            if (itemForOrderFromRepo == null)
            {
                var itemDto = new ItemForUpdateDto();
                patchDoc.ApplyTo(itemDto);
                              
                var validationResult = _validationService.ValidateItemUpdate(itemDto);
                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.ValidationErrors.AddRange(validationResult.Errors.Select(e => new ValidationResult(e.ErrorMessage)));
                    return result;
                }

                var itemToAdd = _mapper.Map<Item>(itemDto);
                itemToAdd.ItemId = itemId;

                await _unitOfWork.Items.AddItemForOrderAsync(orderId, itemToAdd);

                if (!await _unitOfWork.SaveAsync())
                {
                    result.Success = false;
                    throw new Exception($"Upserting item {itemId} for order {orderId} failed on save.");
                }

                result.ItemUpserted = _itemLinksBuilder.CreateDocumentationLinksForItem(_mapper.Map<ItemDto>(itemToAdd));
                result.Success = true;

                return result;
            }

            var itemToPatch = _mapper.Map<ItemForUpdateDto>(itemForOrderFromRepo);
            patchDoc.ApplyTo(itemToPatch);
                        
            var patchValidationResult = _validationService.ValidateItemUpdate(itemToPatch);
            if (!patchValidationResult.IsValid)
            {
                result.Success = false;
                result.ValidationErrors.AddRange(patchValidationResult.Errors.Select(e => new ValidationResult(e.ErrorMessage)));
                return result;
            }

            _mapper.Map(itemToPatch, itemForOrderFromRepo);
            await _unitOfWork.Items.UpdateItemForOrderAsync(itemForOrderFromRepo);

            if (!await _unitOfWork.SaveAsync())
            {
                result.Success = false;
                throw new Exception($"Patching item {itemId} for order {orderId} failed on save.");
            }

            result.ItemUpserted = _itemLinksBuilder.CreateDocumentationLinksForItem(_mapper.Map<ItemDto>(itemForOrderFromRepo));
            result.Success = true;
            return result;
        }
    }
}
