﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Data;
using TinyShop.Infrastructure;
using TinyShop.Models;
using TinyShop.Models.ViewModels;
using TinyShop.RestUtils.NovaPoshta;

namespace TinyShop.Controllers
{
    [Authorize]
    [Route( "[controller]/[action]" )]
    public class OrderController : Controller
    {
        public OrderController( ShopContext context, IDeliveryAddressProvider deliveryAddressProvider, Cart cart )
        {
            _context = context;            
            _cart = cart;
            _deliveryAddressProvider = deliveryAddressProvider;
        }
        
        public async Task<ViewResult> Index()
        {
            return View( await _context.Orders.Where( o => o.SoftDeletedAt.HasValue == false )
                .OrderByDescending( o => o.OrderDateTime ).ToListAsync() );
        }

        [AllowAnonymous]
        public async Task<ViewResult> Checkout()
        {
            var orderVM = new OrderViewModel();            
            orderVM.DeliveryTypes = await _context.DeliveryTypes
                .Where( d => d.SoftDeletedAt.HasValue == false ).OrderBy( d => d.SortingColumn ).ToListAsync();
            orderVM.PaymentTypes = await _context.PaymentTypes
                .Where( p => p.SoftDeletedAt.HasValue == false ).OrderBy( p => p.SortingColumn ).ToListAsync();            
            return View( orderVM );
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Checkout(OrderViewModel orderVM)
        {
            if (_cart.Lines.Count == 0)
            {
                ModelState.AddModelError( string.Empty, "Ваш кошик пустий" );
            }
            if (ModelState.IsValid)
            {
                Order order = new Order();
                await PrepareOrderForInsert( orderVM, order );
                PrepareCustomerForInsertUpdate( orderVM, order );
                await PrepareDeliveryAddressForInsertUpdate( orderVM, order );
                _context.Orders.Add( order );
                await _context.SaveChangesAsync();

                _cart.Clear();
                orderVM.OrderId = order.Id;
                return View( "Completed", orderVM );
            }
            else
            {
                orderVM.DeliveryTypes = await _context.DeliveryTypes
                    .Where( d => d.SoftDeletedAt.HasValue == false ).OrderBy( d => d.SortingColumn ).ToListAsync();
                orderVM.PaymentTypes = await _context.PaymentTypes
                    .Where( p => p.SoftDeletedAt.HasValue == false ).OrderBy( p => p.SortingColumn ).ToListAsync();
                return View(orderVM);
            }
        }

        private async Task PrepareOrderForInsert( OrderViewModel source, Order target )
        {
            target.SetCreateStamp( User?.Identity?.Name );
            target.TheOrderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(
                s => s.Id == OrderStatus.NewId );
            target.TheDeliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync(
                d => d.Id == source.DeliveryTypeId );
            target.ThePaymentType = await _context.PaymentTypes.FirstOrDefaultAsync(
                p => p.Id == source.PaymentTypeId );
            target.Comments = source.Comments;
            target.Lines = _cart.Lines;
            // we need to get the product from the db to avoid the error:
            // "SqlException: Cannot insert explicit value for identity column in table 'Products' when
            // IDENTITY_INSERT is set to OFF."
            foreach ( var line in target.Lines )
            {
                line.TheProduct = await _context.Products.FirstOrDefaultAsync( p => p.Id == line.TheProduct.Id );
                line.SetCreateStamp( User?.Identity?.Name );
            }
            target.OrderDateTime = DateTime.Now;            
        }

        private void PrepareCustomerForInsertUpdate( OrderViewModel source, Order target )
        {
            // LINQ doesn't work here so use loop (return null)
            //var foundCustomer = _context.Customers.FirstOrDefault( c => c.Equals( source.TheCustomer ) );            
            Customer foundCustomer = null;
            foreach ( var customer in _context.Customers )
            {
                if ( customer.Equals( source.TheCustomer ) )
                {
                    foundCustomer = customer;                    
                    break;
                }
            }            
            if ( foundCustomer == null )
            {
                // it's a new customer so we need to add it to the db
                target.TheCustomer.FirstName = source.TheCustomer.FirstName.Trim();
                target.TheCustomer.MiddleName = source.TheCustomer.MiddleName?.Trim();
                target.TheCustomer.LastName = source.TheCustomer.LastName.Trim();
                target.TheCustomer.Email = source.TheCustomer.Email?.Trim();
                target.TheCustomer.Phone = source.TheCustomer.Phone.Trim();
                target.TheCustomer.SetCreateStamp( User?.Identity?.Name );
                _context.Customers.Add( target.TheCustomer );
            }
            else
            {
                // customer already exists in the db so use it                
                bool emailIsNotEmpty = string.IsNullOrEmpty( source.TheCustomer.Email?.Trim() ) == false;
                if ( emailIsNotEmpty )
                {
                    foundCustomer.Email = source.TheCustomer.Email?.Trim();
                }
                foundCustomer.Phone = source.TheCustomer.Phone.Trim();// every time we update the phone to store actual data
                foundCustomer.SetUpdateStamp( User?.Identity?.Name );
                //EntityEntry<Customer> entry = _context.Entry( foundCustomer );
                //entry.State = EntityState.Modified;
                //_context.Entry( foundCustomer ).State = EntityState.Modified;
                _context.Customers.Update( foundCustomer );                
            }
        }
        private async Task PrepareDeliveryAddressForInsertUpdate( OrderViewModel source, Order target )
        {
            if ( source.RegionId == null )// region not selected - no address (for example, pickup)
            {
                return;
            }            
            var foundAddress = await _context.DeliveryAddresses.FirstOrDefaultAsync( 
                a => a.TheRegion.Id.Equals( source.RegionId ) 
                && a.TheCity.Id.Equals( source.CityId ) 
                && a.TheWarehouse.Id.Equals( source.WarehouseId ) 
                && a.SoftDeletedAt.HasValue == false );
            if ( foundAddress == null )
            {
                target.TheDeliveryAddress = new DeliveryAddress();
                if ( source.RegionId != null )
                {
                    target.TheDeliveryAddress.TheRegion = await _context.Regions.FirstOrDefaultAsync(
                        r => r.Id == source.RegionId );
                    target.TheDeliveryAddress.RegionNameSnapshot = target.TheDeliveryAddress?.TheRegion?.Name;
                }
                if ( source.CityId != null )
                {
                    target.TheDeliveryAddress.TheCity = await _context.Cities.FirstOrDefaultAsync(
                        c => c.Id == source.CityId );
                    target.TheDeliveryAddress.CityNameSnapshot = target.TheDeliveryAddress?.TheCity?.Name;
                }
                if ( source.WarehouseId != null )
                {
                    target.TheDeliveryAddress.TheWarehouse = await _context.Warehouses.FirstOrDefaultAsync(
                        w => w.Id == source.WarehouseId );
                    target.TheDeliveryAddress.WarehouseNameSnapshot = target.TheDeliveryAddress?.TheWarehouse?.Name;
                }
                target.TheDeliveryAddress.SetCreateStamp( User?.Identity?.Name );
                _context.DeliveryAddresses.Add( target.TheDeliveryAddress );
            }
            else
            {
                target.TheDeliveryAddress = foundAddress;
            }
        }

        [AllowAnonymous]
        [Route( "{deliveryTypeId}" )]
        public async Task<JsonResult> GetRegions( int deliveryTypeId )
        {
            return Json( await _deliveryAddressProvider.GetRegionsAsync( deliveryTypeId ) );
        }

        [AllowAnonymous]
        [Route( "{deliveryTypeId}/{regionId}" )]
        public async Task<JsonResult> GetCitiesByRegion( int deliveryTypeId, int regionId )
        {
            return Json( await _deliveryAddressProvider.GetCitiesByRegionAsync( deliveryTypeId, regionId ) );
        }

        [AllowAnonymous]
        [Route( "{deliveryTypeId}/{cityId}" )]
        public async Task<JsonResult> GetWarehousesByCity( int deliveryTypeId, int cityId )
        {            
            return Json( await _deliveryAddressProvider.GetWarehousesByCityAsync( deliveryTypeId, cityId ) );
        }

        [AllowAnonymous]
        [Route( "{deliveryTypeId}" )]
        public int GetDefaultRegionId( int deliveryTypeId )
        {
            if ( deliveryTypeId == DeliveryType.NovaPoshtaWarehouseId )
            {
                return Order.NovaPoshtaDefaultRegionId;
            }
            return 0;
        }
        
        private readonly ShopContext _context;
        private readonly IDeliveryAddressProvider _deliveryAddressProvider;
        private readonly Cart _cart;
    }
}