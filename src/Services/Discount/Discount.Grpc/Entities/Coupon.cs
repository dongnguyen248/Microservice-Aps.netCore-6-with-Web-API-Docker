namespace Discount.Grpc.Entities
{
    public class Coupon
    {
        public int ID { get; set; }
        public string ProducName { get; set; } = null!;
        public string? Description { get; set; }
        public int Amount { get; set; }
    }
}
