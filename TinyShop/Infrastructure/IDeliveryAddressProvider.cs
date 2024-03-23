using System.Collections.Generic;
using System.Threading.Tasks;
using TinyShop.Models;

namespace TinyShop.Infrastructure
{
    public interface IDeliveryAddressProvider
    {
        Task<List<Region>> GetRegionsAsync( int deliveryTypeId );
        Task<List<City>> GetCitiesByRegionAsync( int regionId );
        Task<List<Warehouse>> GetWarehousesByCityAsync( int cityId );
    }
}
