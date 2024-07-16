using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models.ViewModels
{
#nullable enable
    public class ProductViewModel
    {
        public ProductViewModel()
        {            
            ProductGroups = new List<ProductGroup>();
        }        
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Введіть назву товару"), Display(Name = "Назва")]
        public string ProductName { get; set; } = string.Empty;
        
        [Display(Name = "Опис")]
        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "Введіть ціну товару"), Display(Name = "Ціна")]
        public decimal? ProductPrice { get; set; }

        [Display(Name = "Група")]
        public int ProductGroupId { get; set; }

        public List<ProductGroup> ProductGroups { get; set; }
        public IFormFileCollection? Photos { get; set; }

        public ICollection<FileTag>? DescImages { get; set; }
    }
#nullable disable
}
