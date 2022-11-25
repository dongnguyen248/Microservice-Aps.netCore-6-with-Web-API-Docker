using Discount.Grpc.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
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
