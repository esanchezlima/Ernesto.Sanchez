using AutoMapper;
using Ernesto.Sanchez.OrderService.Application.Interfaces;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Helpers.LinksBuilders;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Application.Services
{
    public partial class LibraryApplicationService : ILibraryApplicationService
    {
        private readonly OrderUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;
        private readonly IOrderLinksBuilder _orderLinksBuilder;
        private readonly IItemLinksBuilder _itemLinksBuilder;
        private readonly IRootLinksBuilder _rootLinksBuilder;

        public LibraryApplicationService(
            OrderUnitOfWork unitOfWork,
            IMapper mapper,
            IOrderLinksBuilder orderLinksBuilder,
            IItemLinksBuilder itemLinksBuilder,
            IRootLinksBuilder rootLinksBuilder,
            IValidationService validationService
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderLinksBuilder = orderLinksBuilder;
            _itemLinksBuilder = itemLinksBuilder;
            _rootLinksBuilder = rootLinksBuilder;
            _validationService = validationService;
        }
    }
}


