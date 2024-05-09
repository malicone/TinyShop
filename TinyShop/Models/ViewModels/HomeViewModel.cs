using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public int CurrentProductGroupId { get; set; }
        public PagingInfo PagingInfo { get; set; } = new();
    }
}
