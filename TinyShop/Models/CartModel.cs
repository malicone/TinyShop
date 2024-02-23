namespace TinyShop.Models
{
    public class CartModel
    {
        public CartModel(Cart cartService)
        {
            Cart = cartService;
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
