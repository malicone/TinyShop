using System.Collections.Generic;
using System.Threading.Tasks;
using TinyShop.Models;

namespace TinyShop.RestUtils.Common
{
    public abstract class DeliveryRestClientAbstract
    {
        public DeliveryRestClientAbstract()
        {

        }
        public abstract string BaseUrl { get; protected set; }
        public abstract string ApiKey { get; protected set; }

        public abstract Task<List<WarehouseType>> GetWarehouseTypesAsync();
        public abstract Task<List<Region>> GetRegionsAllAsync();        
        public abstract Task<List<City>> GetCitiesAllAsync();        
        public abstract Task<List<Warehouse>> GetWarehousesAllAsync();
    }
}
