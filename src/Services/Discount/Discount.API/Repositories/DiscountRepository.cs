using Discount.API.Data;
using Discount.API.Entities;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        public DiscountRepository(DiscountContext discountContext)
        {
            
            _discountContext = discountContext;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            try
            {
                _discountContext.Coupons.Add(coupon);
                _discountContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            try
            {
               var discount =  _discountContext.Coupons.FindAsync(productName);
                //await _discountContext.Coupons.Remove(discount.);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var coupon =  _discountContext.Coupons.Find(productName);
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
