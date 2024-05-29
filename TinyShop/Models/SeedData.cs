using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TinyShop.Data;

namespace TinyShop.Models
{
    /// <summary>
    /// Other tables are seeded in migrations.
    /// </summary>
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ShopContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<ShopContext>();

            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if(context.ProductUnitTypes.Any() == false)
            {
                context.ProductUnitTypes.AddRange
                (
                    new ProductUnitType
                    {
                        Name = "шт.",
                        Description = "Штука",
                        SortingColumn = 100
                    },
                    new ProductUnitType
                    {
                        Name = "пара",
                        Description = "Пара",
                        SortingColumn = 200
                    }
                );
                foreach(var unitType in context.ProductUnitTypes)
                {
                    unitType.SetCreateStamp();
                }
            }
            var colorProperty = new ProductProperty
            {
                Name = "Колір",
                Description = "Виберіть колір товару",
                SortingColumn = 100
            };
            colorProperty.SetCreateStamp();
            var clothSizePropertyUsa = new ProductProperty
            {
                Name = "Розмір",
                Description = "Розмір одягу (S, M, L)",
                SortingColumn = 200
            };
            clothSizePropertyUsa.SetCreateStamp();
            var clothSizePropertyUa = new ProductProperty
            {
                Name = "Розмір",
                Description = "Розмір одягу (46, 48, 50)",
                SortingColumn = 300
            };
            clothSizePropertyUa.SetCreateStamp();
            var shoeSizeProperty = new ProductProperty
            {
                Name = "Розмір",
                Description = "Розмір взуття (37, 38, 39)",
                SortingColumn = 400
            };
            shoeSizeProperty.SetCreateStamp();
            var sockSizeProperty = new ProductProperty
            {
                Name = "Розмір",
                Description = "Розмір шкарпеток (25-27, 27-29)",
                SortingColumn = 500
            };
            sockSizeProperty.SetCreateStamp();
            if(context.ProductProperties.Any() == false)
            {
                context.ProductProperties.AddRange
                (
                    colorProperty,
                    clothSizePropertyUsa,
                    clothSizePropertyUa,
                    shoeSizeProperty,
                    sockSizeProperty
                );
            }
            if(context.ProductPropertyItems.Any() == false)
            {
                string[] clothSizesUsa = { "XS", "S", "S/M", "M", "L", "L/XL", "XL", "XXL", "XXXL", "4XL", "5XL", 
                    "6XL", "7XL", "8XL", "9XL", "10XL" };
                int sortingColumnCounter = 100;
                foreach ( string size in clothSizesUsa )
                {
                    var item = new ProductPropertyItem();
                    item.SetCreateStamp();
                    item.Name = size;
                    item.TheProductProperty = clothSizePropertyUsa;
                    item.SortingColumn = sortingColumnCounter;
                    context.ProductPropertyItems.Add( item );
                    sortingColumnCounter += 100;
                }

                string[] sizes = { "40", "42", "44", "46", "48", "50", "52", "54", "56", "58" };
                sortingColumnCounter = 100;
                foreach ( string size in sizes )
                {
                    var item = new ProductPropertyItem();
                    item.SetCreateStamp();
                    item.Name = size;
                    item.TheProductProperty = clothSizePropertyUa;
                    item.SortingColumn = sortingColumnCounter;
                    context.ProductPropertyItems.Add( item );
                    sortingColumnCounter += 100;
                }

                string[] shoeSizes = { "35", "36", "37", "38", "39", "40", "40-41", "41", "42", "42-43", "43", "44", 
                    "45", "46" };
                sortingColumnCounter = 100;
                foreach ( string size in shoeSizes )
                {
                    var item = new ProductPropertyItem
                    {
                        Name = size,
                        TheProductProperty = shoeSizeProperty,
                        SortingColumn = sortingColumnCounter
                    };
                    item.SetCreateStamp();
                    context.ProductPropertyItems.Add( item );
                    sortingColumnCounter += 100;
                }

                sortingColumnCounter = 100;
                string[] sockSizes = {
                    "23-25", "25-27", "27-29", "29-31", "35-39", "35-41", "36-38", "36-39",
                    "36-40", "36-41", "37-41", "37-42", "40-45", "41-44", "41-45", "41-46",
                    "41-47", "42-44"
                };
                foreach ( string size in sockSizes )
                {
                    var item = new ProductPropertyItem();
                    item.SetCreateStamp();
                    item.Name = size;
                    item.TheProductProperty = sockSizeProperty;
                    item.SortingColumn = sortingColumnCounter;
                    context.ProductPropertyItems.Add( item );
                    sortingColumnCounter += 100;
                }
            }
            context.SaveChanges();
        }
    }
}