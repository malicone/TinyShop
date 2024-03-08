namespace TinyShop.Models.ViewModels
{
    public class CartViewModel
    {
        public CartViewModel(Cart cartService)
        {
            Cart = cartService;
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
