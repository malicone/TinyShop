using System.Collections.Generic;
using System.Threading.Tasks;
using TinyShop.RestUtils.Common.Dto;

namespace TinyShop.RestUtils.Common
{
    public abstract class DeliveryRestClientAbstract
    {
        public DeliveryRestClientAbstract()
        {

        }
        public abstract string BaseUrl { get; protected set; }
        public abstract string ApiKey { get; protected set; }

        public abstract Task<List<RegionDto>> GetRegionsAsync();
        public abstract Task<List<CityDto>> GetCitiesByRegionAsync(string regionId);
        public abstract Task<List<WarehouseDto>> GetWarehousesByCityAsync(string cityId);
    }
}
