namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.QueryParametersTypes
{
    public class QueryParameterListGuidsType
    {
        public List<Guid> Items { get; set; } = new List<Guid>();

        public static bool TryParse(string? value, IFormatProvider? provider, out QueryParameterListGuidsType? items)
        {
            var segments = value?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (segments == null)
            {
                items = new QueryParameterListGuidsType();
                return false;
            }

            var itemsList = new List<Guid>();
            foreach (var segment in segments)
            {
                if (Guid.TryParse(segment, out var id))
                {
                    itemsList.Add(id);
                }
            }

            items = new QueryParameterListGuidsType()
            {
                Items = itemsList
            };

            return true;
        }
    }    
}
