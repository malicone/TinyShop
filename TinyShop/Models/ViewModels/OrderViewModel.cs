using System.Collections.Generic;
using TinyShop.RestUtils.Common.Dto;

namespace TinyShop.Models.ViewModels
{
    public class OrderViewModel
    {
        public Order TheOrder { get; set; }
        public List<RegionDto> Regions { get; set; } = new List<RegionDto>();
    }
}
