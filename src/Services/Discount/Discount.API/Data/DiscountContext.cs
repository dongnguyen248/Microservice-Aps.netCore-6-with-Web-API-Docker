using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Data
{
    public class DiscountContext:DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
      public  DbSet<Coupon> Coupons { get; set; }
    }
}
