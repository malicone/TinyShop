using System.Collections.Generic;

namespace TinyShop.Models.ViewModels
{
    public class ProductGroupViewModel
    {
        public List<ProductGroup> ProductGroups { get; set; }        
        public int SelectedGroupId { get; set; }
    }
}
