namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base
{
    public class LinkedCollectionResource
    {
        public IEnumerable<IDictionary<string, object>> Value { get; set; }
        public IEnumerable<LinkDto> Links { get; set; }
    }
}
