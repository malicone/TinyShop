using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            TheProduct = new Product();
            ProductGroups = new List<ProductGroup>();
        }
        public Product TheProduct { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }
        public IFormFileCollection Photos { get; set; }
    }
}
