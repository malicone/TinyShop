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
            var deliveryTypeFromDb = await _context.DeliveryTypes.FirstOrDefaultAsync(
                x => x.Id == DeliveryType.NovaPoshtaWarehouseId );
            foreach (var currentType in warehouseTypes)
            {
                var typeFromDb = await _context.WarehouseTypes.FirstOrDefaultAsync(
                    x => x.IdExternal == currentType.IdExternal );
                if ( typeFromDb == null )
                {
                    currentType.SetCreateStamp( User.Identity.Name );
                    currentType.TheDeliveryType = deliveryTypeFromDb;
                    _context.WarehouseTypes.Add( currentType );

                }
                else
                {
                    typeFromDb.SetUpdateStamp( User.Identity.Name );
                    _context.WarehouseTypes.Update( typeFromDb );
                }
            }
            _context.SaveChanges();
        }

        private async Task RefreshNPRegions()
        {
            var regions = await _npClient.GetRegionsAsync();
            var deliveryTypeFromDb = await _context.DeliveryTypes.FirstOrDefaultAsync(
                x => x.Id == DeliveryType.NovaPoshtaWarehouseId );
            foreach ( var currentRegion in regions)
            {
                var regionFromDb = await _context.Regions.FirstOrDefaultAsync(
                    x => x.IdExternal == currentRegion.IdExternal );
                if ( regionFromDb == null )
                {
                    currentRegion.SetCreateStamp( User.Identity.Name );
                    currentRegion.TheDeliveryType = deliveryTypeFromDb;
                    _context.Regions.Add( currentRegion );

                }
                else
                {
                    regionFromDb.SetUpdateStamp( User.Identity.Name );
                    _context.Regions.Update( regionFromDb );
                }
            }
            _context.SaveChanges();
        }

        private async Task RefreshNPCities()
        {
            var regions = await _npClient.GetRegionsAsync();
            var deliveryTypeFromDb = await _context.DeliveryTypes.FirstOrDefaultAsync(
                x => x.Id == DeliveryType.NovaPoshtaWarehouseId );
            foreach ( var currentRegion in regions )
            {
                var cities = await _npClient.GetCitiesByRegionAsync( currentRegion.IdExternal );
                var regionFromDb = await _context.Regions.FirstOrDefaultAsync( x => x.IdExternal == currentRegion.IdExternal );
                foreach ( var currentCity in cities )
                {
                    var cityFromDb = await _context.Cities.FirstOrDefaultAsync( x => x.IdExternal == currentCity.IdExternal );
                    if ( cityFromDb == null )
                    {
                        currentCity.SetCreateStamp( User.Identity.Name );
                        currentCity.TheRegion = regionFromDb;
                        currentCity.TheDeliveryType = deliveryTypeFromDb;
                        _context.Cities.Add( currentCity );
                    }
                    else
                    {
                        cityFromDb.SetUpdateStamp( User.Identity.Name );
                        // city can be moved to another region; it's low probability, but it's possible
                        cityFromDb.TheRegion = regionFromDb;
                        _context.Cities.Update( cityFromDb );
                    }
                }
            }
            _context.SaveChanges();
        }

        private async Task RefreshNPWarehouses()
        {
            var cities = await _npClient.GetCitiesAllAsync();
            var deliveryTypeFromDb = await _context.DeliveryTypes.FirstOrDefaultAsync(
                x => x.Id == DeliveryType.NovaPoshtaWarehouseId );
            foreach ( var currentCity in cities )
            {
                var cityFromDb = await _context.Cities.FirstOrDefaultAsync( x => x.IdExternal == currentCity.IdExternal );
                var warehouses = await _npClient.GetWarehousesByCityAsync( currentCity.IdExternal );
                foreach ( var currentWarehouse in warehouses )
                {
                    var warehouseFromDb = await _context.Warehouses.FirstOrDefaultAsync(
                        x => x.IdExternal == currentWarehouse.IdExternal );
                    if ( warehouseFromDb == null )
                    {
                        currentWarehouse.SetCreateStamp( User.Identity.Name );
                        currentWarehouse.TheCity = cityFromDb;
                        currentWarehouse.TheWarehouseType = await _context.WarehouseTypes.FirstOrDefaultAsync(
                            x => x.IdExternal == currentWarehouse.WarehouseTypeIdExternal );
                        currentWarehouse.TheDeliveryType = deliveryTypeFromDb;
                        _context.Warehouses.Add( currentWarehouse );
                    }
                    else
                    {
                        warehouseFromDb.SetUpdateStamp( User.Identity.Name );
                        warehouseFromDb.TheCity = cityFromDb;
                        _context.Warehouses.Update( warehouseFromDb );
                    }
                }
            }
            _context.SaveChanges();
        }

        private readonly ShopContext _context;
        private readonly NovaPoshtaClient _npClient;
    }
}
