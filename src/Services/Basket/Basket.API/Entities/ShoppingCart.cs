namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = null!;
        public List<ShoppingCartitem> ShoppingCartitems { get; set; } = new List<ShoppingCartitem>();

        public ShoppingCart()
        {

        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (ShoppingCartitem item in ShoppingCartitems)
                {
                    totalPrice += item.Price * item.Quantity ;
                }
                return totalPrice;
            }
        }
    }
}
