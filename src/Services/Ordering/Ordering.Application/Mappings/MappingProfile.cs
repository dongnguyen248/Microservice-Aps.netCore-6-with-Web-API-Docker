
using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.ViewModels;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersVM>().ReverseMap();
            CreateMap<Order,CheckOutOrderCommand>().ReverseMap();
        }
    }
}
