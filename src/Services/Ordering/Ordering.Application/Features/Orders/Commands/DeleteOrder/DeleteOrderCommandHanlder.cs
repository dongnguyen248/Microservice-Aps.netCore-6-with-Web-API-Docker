using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHanlder : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommandHanlder> _logger;

        public DeleteOrderCommandHanlder(IOrderRepository orderRepository,IMapper mapper, ILogger<DeleteOrderCommandHanlder> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
          var oderToDelete = _orderRepository.GetByIdAsync(request.Id);
            if (oderToDelete == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }
                await _orderRepository.DeleteAsync(oderToDelete.Id);
            _logger.LogInformation($"Order {oderToDelete.Id} is successfully deleted.");
            return Unit.Value;
        }
    }
}
