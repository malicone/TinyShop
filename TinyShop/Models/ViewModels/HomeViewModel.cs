using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }

        public ProductGroup CurrentGroup { get; set; }
        public string CatalogHeader
        {
            get
            {
                string name = CurrentGroup == null ? "Усі товари" : CurrentGroup.Name;
                return $"Каталог - {name}";
            }
        }
    }
}
