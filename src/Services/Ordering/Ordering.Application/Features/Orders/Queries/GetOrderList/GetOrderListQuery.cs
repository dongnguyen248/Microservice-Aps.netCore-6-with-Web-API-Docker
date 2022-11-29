using MediatR;
using Ordering.Application.ViewModels;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQuery:IRequest<List<OrdersVM>>
    {
        public string UserName { get; set; }

        public GetOrderListQuery(string userName)
        {
            UserName = userName;

        }
    }
}
