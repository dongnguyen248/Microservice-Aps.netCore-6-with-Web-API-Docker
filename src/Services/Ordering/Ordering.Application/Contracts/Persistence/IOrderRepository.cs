using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository:IGenericAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOderByUserName(string userName);
        
    }
}
