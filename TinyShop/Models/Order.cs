using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TinyShop.RestUtils.Common.Dto;

namespace TinyShop.Models
{
    public class Order
    {
        [BindNever]
        public int Id { get; set; }
        [Required( ErrorMessage = "Please enter a First Name" )]
        public string FirstName { get; set; }
        [Required( ErrorMessage = "Please enter a Last Name" )]
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

        [Display( Name = "Region" )]
        public string RegionId { get; set; }
        public List<RegionDto> Regions { get; set; } = new List<RegionDto>();

        public string Content { get; set; }
    }
}
