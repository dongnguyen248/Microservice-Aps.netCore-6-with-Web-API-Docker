using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.ViewModels;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrdersVM>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<List<OrdersVM>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
           var  orderList = await _orderRepository.GetOderByUserName(request.UserName);
            return _mapper.Map<List<OrdersVM>>(orderList);
        }
    }
}
