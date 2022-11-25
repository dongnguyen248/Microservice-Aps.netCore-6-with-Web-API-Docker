using Discount.Grpc.Data;
using Discount.Grpc.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        public DiscountRepository(DiscountContext discountContext)
        {
            _discountContext = discountContext;
        }
        public Task<bool> CreateDiscount(Coupon coupon)
        {
            _discountContext.Coupons.Add(coupon);
            _discountContext.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> DeleteDiscount(string productName)
        {
            _discountContext.Remove(productName);
            _discountContext.SaveChanges(true);
            return Task.FromResult(true);
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var discount = await _discountContext.Coupons.FirstOrDefaultAsync(x => x.ProducName == productName);
            if(discount == null)
            {
                 throw new InvalidOperationException("product no have found discount") ;
            }
            return discount;
        }

        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            _discountContext.Update(coupon);
            _discountContext.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
