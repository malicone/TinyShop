using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Data;
using TinyShop.Models;
using TinyShop.Models.ViewModels;
using TinyShop.RestUtils.NovaPoshta;

namespace TinyShop.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public AdminController( ShopContext context, NovaPoshtaClient npClient )
        {
            _context = context;
            _npClient = npClient;            
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult NovaposhtaIndex()
        {
            return View();
        }
        public async Task<ViewResult> RefreshNPAddresses()
        {
            _refreshNPStartedAt = DateTime.Now;
            RefreshNPAddressesResultViewModel resultVM = new RefreshNPAddressesResultViewModel();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await RefreshNPWarehouseTypes();
                    await RefreshNPRegions();
                    await RefreshNPCities();
                    await RefreshNPWarehouses();
                    transaction.Commit();
                    resultVM.Message = "Адреси відділень Нової Пошти успішно оновлено";
                    return View( "RefreshNPAddressesResult", resultVM );
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ModelState.AddModelError( string.Empty, ex.Message );
                    resultVM.Message = "Сталася помилка під час оновлення адрес відділень Нової Пошти";
                    resultVM.ErrorDescription = ex.Message + "\n" + ex.InnerException.Message;
                    return View( "RefreshNPAddressesResult", resultVM );
                }
            }            
        }

        private async Task RefreshNPWarehouseTypes()
        {
            var warehouseTypes = await _npClient.GetWarehouseTypesAsync();
            var deliveryFirmFromDb = await _context.DeliveryFirms.FirstOrDefaultAsync(
                x => x.Id == DeliveryFirm.NovaPoshtaId );            
            foreach (var currentType in warehouseTypes)
            {
                var warehouseTypeFromDb = await _context.WarehouseTypes.FirstOrDefaultAsync(
                    x => x.IdExternal == currentType.IdExternal );
                if ( warehouseTypeFromDb == null )
                {
                    currentType.SetCreateStamp( User?.Identity?.Name );
                    currentType.TheDeliveryFirm = deliveryFirmFromDb;
                    _context.WarehouseTypes.Add( currentType );

                }
                else
                {
                    warehouseTypeFromDb.SetUpdateStamp( User?.Identity?.Name );
                    _context.WarehouseTypes.Update( warehouseTypeFromDb );
                }
            }
            _context.SaveChanges();
            // now remove all garbage records (those that were not created or updated during this refresh)
            _context.WarehouseTypes.RemoveRange( _context.WarehouseTypes
                .Where( t => ( t.UpdatedAt.HasValue == false ) && ( t.CreatedAt < _refreshNPStartedAt ) ) );
            _context.WarehouseTypes.RemoveRange( _context.WarehouseTypes
                .Where( t => ( t.UpdatedAt.HasValue ) && ( t.UpdatedAt < _refreshNPStartedAt ) ) );
            _context.SaveChanges();
        }

        private async Task RefreshNPRegions()
        {
            var regions = await _npClient.GetRegionsAllAsync();
            var deliveryFirmFromDb = await _context.DeliveryFirms.FirstOrDefaultAsync(
                x => x.Id == DeliveryFirm.NovaPoshtaId );
            foreach ( var currentRegion in regions)
            {
                var regionFromDb = await _context.Regions.FirstOrDefaultAsync(
                    x => x.IdExternal == currentRegion.IdExternal );
                if ( regionFromDb == null )
                {
                    currentRegion.SetCreateStamp( User?.Identity?.Name );
                    currentRegion.TheDeliveryFirm = deliveryFirmFromDb;
                    _context.Regions.Add( currentRegion );

                }
                else
                {
                    regionFromDb.SetUpdateStamp( User?.Identity?.Name );
                    _context.Regions.Update( regionFromDb );
                }
            }
            _context.SaveChanges();
            _context.Regions.RemoveRange( _context.Regions
                .Where( r => ( r.UpdatedAt.HasValue == false ) && ( r.CreatedAt < _refreshNPStartedAt ) ) );
            _context.Regions.RemoveRange( _context.Regions
                .Where( r => ( r.UpdatedAt.HasValue ) && ( r.UpdatedAt < _refreshNPStartedAt ) ) );
            _context.SaveChanges();
        }

        private async Task RefreshNPCities()
        {
            var cities = await _npClient.GetCitiesAllAsync();
            var deliveryFirmFromDb = await _context.DeliveryFirms.FirstOrDefaultAsync(
                x => x.Id == DeliveryFirm.NovaPoshtaId );
            foreach ( var currentCity in cities )
            {
                var regionFromDb = await _context.Regions.FirstOrDefaultAsync(
                    x => x.IdExternal == currentCity.RegionIdExternal );
                var cityFromDb = await _context.Cities.FirstOrDefaultAsync( x => x.IdExternal == currentCity.IdExternal );
                if ( cityFromDb == null )
                {
                    currentCity.SetCreateStamp( User?.Identity?.Name );
                    currentCity.TheRegion = regionFromDb;
                    currentCity.TheDeliveryFirm = deliveryFirmFromDb;
                    _context.Cities.Add( currentCity );
                }
                else
                {
                    cityFromDb.SetUpdateStamp( User?.Identity?.Name );
                    // city can be moved to another region; it's low probability, but it's possible
                    cityFromDb.TheRegion = regionFromDb;
                    _context.Cities.Update( cityFromDb );
                }
            }
            _context.SaveChanges();
            _context.Cities.RemoveRange( _context.Cities
                .Where( c => ( c.UpdatedAt.HasValue == false ) && ( c.CreatedAt < _refreshNPStartedAt ) ) );
            _context.Cities.RemoveRange( _context.Cities
                .Where( c => ( c.UpdatedAt.HasValue ) && ( c.UpdatedAt < _refreshNPStartedAt ) ) );
            _context.SaveChanges();
        }

        private async Task RefreshNPWarehouses()
        {
            var deliveryFirmFromDb = await _context.DeliveryFirms.FirstOrDefaultAsync(
                x => x.Id == DeliveryFirm.NovaPoshtaId );
            var warehouses = await _npClient.GetWarehousesAllAsync();
            foreach ( var currentWarehouse in warehouses )
            {
                var warehouseFromDb = await _context.Warehouses.FirstOrDefaultAsync(
                    x => x.IdExternal == currentWarehouse.IdExternal );
                var cityFromDb = await _context.Cities.FirstOrDefaultAsync(
                    x => x.IdExternal == currentWarehouse.CityIdExternal );
                if ( warehouseFromDb == null )
                {
                    currentWarehouse.SetCreateStamp( User?.Identity?.Name );
                    currentWarehouse.TheCity = cityFromDb;
                    currentWarehouse.TheWarehouseType = await _context.WarehouseTypes.FirstOrDefaultAsync(
                        x => x.IdExternal == currentWarehouse.WarehouseTypeIdExternal );
                    currentWarehouse.TheDeliveryFirm = deliveryFirmFromDb;
                    _context.Warehouses.Add( currentWarehouse );
                }
                else
                {
                    warehouseFromDb.SetUpdateStamp( User?.Identity?.Name );
                    warehouseFromDb.TheCity = cityFromDb;
                    _context.Warehouses.Update( warehouseFromDb );
                }
            }
            _context.SaveChanges();
            _context.Warehouses.RemoveRange( _context.Warehouses
                .Where( w => ( w.UpdatedAt.HasValue == false ) && ( w.CreatedAt < _refreshNPStartedAt ) ) );
            _context.Warehouses.RemoveRange( _context.Warehouses
                .Where( w => ( w.UpdatedAt.HasValue ) && ( w.UpdatedAt < _refreshNPStartedAt ) ) );
            _context.SaveChanges();
        }

        private readonly ShopContext _context;
        private readonly NovaPoshtaClient _npClient;
        private DateTime _refreshNPStartedAt;
    }
}
