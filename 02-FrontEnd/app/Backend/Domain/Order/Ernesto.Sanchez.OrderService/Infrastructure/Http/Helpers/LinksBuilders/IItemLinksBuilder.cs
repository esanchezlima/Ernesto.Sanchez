using Ernesto.Sanchez.OrderService.Application.Dtos;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders
{
    public interface IItemLinksBuilder
    {
        ItemDto CreateDocumentationLinksForItem(ItemDto item);
        LinkedCollectionResourceWrapperDto<ItemDto> CreateDocumentationLinksForItems(Guid orderId, LinkedCollectionResourceWrapperDto<ItemDto> itemsWrapper);
    }
}