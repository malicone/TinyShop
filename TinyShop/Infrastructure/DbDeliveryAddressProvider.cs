using Microsoft.EntityFrameworkCore;
using NaturalSort.Extension;
using System;
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
            int? deliveryFirmId = await _context.DeliveryTypes.Where( t => t.Id == deliveryTypeId )
                .Select( t => t.TheDeliveryFirm.Id ).FirstOrDefaultAsync();
            if (deliveryFirmId.HasValue)
            {
                return await _context.Regions.Where( r => ( ( r.TheDeliveryFirm != null )
                    && ( r.TheDeliveryFirm.Id == deliveryFirmId.Value )
                    && ( r.SoftDeletedAt.HasValue == false ) ) ).OrderBy( r => r.Name ).ToListAsync();
            }
            return new List<Region>();
        }

        public async Task<List<City>> GetCitiesByRegionAsync( int deliveryTypeId, int regionId )
        {
            if ( deliveryTypeId == DeliveryType.NovaPoshtaWarehouseId )
            {
                var cities = await _context.Cities.Where( c =>
                    ( c.TheRegion != null )
                    && ( c.TheRegion.Id == regionId )
                    && ( c.SoftDeletedAt.HasValue == false )
                    && ( c.Warehouses.Any(
                        w => ( w.TheWarehouseType != null ) &&
                        ( ( w.TheWarehouseType.Id == WarehouseType.NovaposhtaWarehouse30kgTypeId )
                        || ( w.TheWarehouseType.Id == WarehouseType.NovaposhtaWarehouse1000kgTypeId ) ) )
                        )
                    )
                    .OrderBy( c => c.Name ).ToListAsync();
                return cities;
            }
            return new List<City>();
        }

        public async Task<List<Warehouse>> GetWarehousesByCityAsync( int deliveryTypeId, int cityId )
        {
            if ( deliveryTypeId == DeliveryType.NovaPoshtaWarehouseId )
            {
                var warehouses = await _context.Warehouses.Where( w =>
                    ( w.TheCity != null )
                    && ( w.TheCity.Id == cityId )
                    && ( w.SoftDeletedAt.HasValue == false ) )
                    .Where( w2 =>
                        ( w2.TheWarehouseType != null )
                        && ( ( w2.TheWarehouseType.Id == WarehouseType.NovaposhtaWarehouse30kgTypeId )
                            || ( w2.TheWarehouseType.Id == WarehouseType.NovaposhtaWarehouse1000kgTypeId ) )
                    )
                    .ToListAsync();
                return new List<Warehouse>( warehouses.OrderByNatural( w => w.Name ) );
            }
            return new List<Warehouse>();
        }

        private readonly ShopContext _context;
    }
}
