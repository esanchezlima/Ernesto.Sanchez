using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Results.Root
{
    public class GetRootResult
    {
        public List<LinkDto> LinkedResources { get; set; }
    }
}
