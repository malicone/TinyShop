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
            foreach (var currentType in warehouseTypes)
            {
                if (_context.WarehouseTypes.Any( x => x.IdExternal == currentType.IdExternal ))
                {                     
                    currentType.SetUpdateStamp( User.Identity.Name );
                    _context.WarehouseTypes.Update( currentType );
                }
                else
                {
                    currentType.SetCreateStamp( User.Identity.Name );
                    currentType.TheDeliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync( 
                        x => x.Id == DeliveryType.NovaPoshtaWarehouseId );
                    _context.WarehouseTypes.Add( currentType );
                }
            }
            _context.SaveChanges();
        }

        private async Task RefreshNPRegions()
        {
            var regions = await _npClient.GetRegionsAsync();
            foreach (var currentRegion in regions)
            {
                if (_context.Regions.Any( x => x.IdExternal == currentRegion.IdExternal ))
                {
                    currentRegion.SetUpdateStamp( User.Identity.Name );
                    _context.Regions.Update( currentRegion );
                }
                else
                {
                    currentRegion.SetCreateStamp( User.Identity.Name );
                    currentRegion.TheDeliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync(
                        x => x.Id == DeliveryType.NovaPoshtaWarehouseId );
                    _context.Regions.Add( currentRegion );
                }
            }
            _context.SaveChanges();
        }

        private async Task RefreshNPCities()
        {
            var regions = await _npClient.GetRegionsAsync();
            foreach ( var currentRegion in regions )
            {
                var cities = await _npClient.GetCitiesByRegionAsync( currentRegion.IdExternal );
                foreach ( var currentCity in cities )
                {
                    if ( _context.Cities.Any( x => x.IdExternal == currentCity.IdExternal ) )
                    {
                        currentCity.SetUpdateStamp( User.Identity.Name );
                        _context.Cities.Update( currentCity );
                    }
                    else
                    {
                        currentCity.SetCreateStamp( User.Identity.Name );
                        currentCity.TheRegion = await _context.Regions.FirstOrDefaultAsync(
                            x => x.IdExternal == currentRegion.IdExternal );
                        currentCity.TheDeliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync(
                            x => x.Id == DeliveryType.NovaPoshtaWarehouseId );
                        _context.Cities.Add( currentCity );
                    }
                }
            }
            _context.SaveChanges();
        }

        private async Task RefreshNPWarehouses()
        {
            var cities = await _npClient.GetCitiesAllAsync();
            foreach ( var currentCity in cities )
            {
                var warehouses = await _npClient.GetWarehousesByCityAsync( currentCity.IdExternal );
                foreach ( var currentWarehouse in warehouses )
                {
                    if ( _context.Warehouses.Any( x => x.IdExternal == currentWarehouse.IdExternal ) )
                    {
                        currentWarehouse.SetUpdateStamp( User.Identity.Name );
                        _context.Warehouses.Update( currentWarehouse );
                    }
                    else
                    {
                        currentWarehouse.SetCreateStamp( User.Identity.Name );
                        currentWarehouse.TheCity = await _context.Cities.FirstOrDefaultAsync(
                            x => x.IdExternal == currentCity.IdExternal );
                        currentWarehouse.TheWarehouseType = await _context.WarehouseTypes.FirstOrDefaultAsync(
                            x => x.IdExternal == currentWarehouse.WarehouseTypeIdExternal );
                        currentWarehouse.TheDeliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync(
                            x => x.Id == DeliveryType.NovaPoshtaWarehouseId );
                        _context.Warehouses.Add( currentWarehouse );
                    }
                }
            }
            _context.SaveChanges();
        }

        private readonly ShopContext _context;
        private readonly NovaPoshtaClient _npClient;
    }
}
