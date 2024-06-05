using Ernesto.Sanchez.OrderService.Application.Interfaces;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Root;

namespace Ernesto.Sanchez.OrderService.Application.Services
{
    public partial class LibraryApplicationService : ILibraryApplicationService
    {        
        public GetRootResult GetRoot()
        {
            GetRootResult result = new GetRootResult();
            result.LinkedResources = _rootLinksBuilder.CreateDocumentationLinksForRoot();            
            return result;
        }
        public async Task<GetRootResult> GetRootAsync()
        {
            GetRootResult result = new GetRootResult();
            result.LinkedResources = await Task.FromResult(_rootLinksBuilder.CreateDocumentationLinksForRoot());
            return result;
        }

    }
}
