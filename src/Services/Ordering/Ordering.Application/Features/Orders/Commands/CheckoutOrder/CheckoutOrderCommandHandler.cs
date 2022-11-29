using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrstructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckOutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRespository;
        private readonly IMapper _mapper;
        private readonly ISendMailService _sendEmail;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRespository,IMapper mapper, ISendMailService sendEmail,ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRespository = orderRespository;
            _mapper = mapper;
            _sendEmail = sendEmail;
            _logger = logger;
        }
        public async Task<int> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
        {
            var newOrder = await _orderRespository.AddAsync(_mapper.Map<Order>(request);
            _logger.LogInformation($"Order {newOrder.id} is successfull createdd");
            await SendMail(newOrder);
            return newOrder.id;
        }

        private async Task SendMail(Order newOrder)
        {
            var email = new Email()
            {
                To = "dongnguyen.na1@gmail.com",
                Body = $"Order was created. {newOrder}",
                Subject = "Order was created."
            };
            try
            {
                await _sendEmail.SendMail(email);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Order {newOrder.Id} fail to due to mail service:{ex.Message} ");
            }
        }
    }
}
