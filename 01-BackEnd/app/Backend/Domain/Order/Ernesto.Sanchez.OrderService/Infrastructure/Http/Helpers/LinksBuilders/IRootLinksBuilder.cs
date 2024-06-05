using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;
using System.Collections.Generic;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders
{
    public interface IRootLinksBuilder
    {
        List<LinkDto> CreateDocumentationLinksForRoot();
    }
}