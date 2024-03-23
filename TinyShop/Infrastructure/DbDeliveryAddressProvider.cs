using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Data;
using TinyShop.Models;

namespace TinyShop.Infrastructure
{
    public class DbDeliveryAddressProvider : IDeliveryAddressProvider
    {
        public DbDeliveryAddressProvider( ShopContext context ) 
        { 
            _context = context;
        }
        public async Task<List<Region>> GetRegionsAsync( int deliveryTypeId )
        {
            return await _context.Regions.Where( r => ( r.TheDeliveryType.Id == deliveryTypeId )
                && ( r.SoftDeletedAt.HasValue == false ) ).OrderBy( r => r.Name ).ToListAsync();
        }

        public async Task<List<City>> GetCitiesByRegionAsync( int regionId )
        {
            return await _context.Cities.Where( c => ( c.TheRegion.Id == regionId )
                && ( c.SoftDeletedAt.HasValue == false ) ).OrderBy( c => c.Name ).ToListAsync();
        }

        public async Task<List<Warehouse>> GetWarehousesByCityAsync( int cityId )
        {
            return await _context.Warehouses.Where( w => ( w.TheCity.Id == cityId )
                && ( w.SoftDeletedAt.HasValue == false ) ).OrderBy( w => w.Name ).ToListAsync();
        }

        private readonly ShopContext _context;
    }
}
