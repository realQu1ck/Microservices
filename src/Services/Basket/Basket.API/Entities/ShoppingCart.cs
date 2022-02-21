namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string? UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

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
                decimal sumPrice = 0;
                foreach (ShoppingCartItem item in Items)
                {
                    sumPrice += item.Price * item.Count;
                }
                return sumPrice;
            }
        }
    }
}
