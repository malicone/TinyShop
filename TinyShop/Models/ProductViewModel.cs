using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
    public class ProductViewModel
    {
        public Product TheProduct { get; set; }
        public IFormFileCollection Photos { get; set; }
    }
}
