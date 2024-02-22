namespace TinyShop.Models
{
    public class CartModel
    {
        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
