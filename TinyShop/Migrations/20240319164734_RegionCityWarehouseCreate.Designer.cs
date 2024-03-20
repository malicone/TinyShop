﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TinyShop.Data;

namespace TinyShop.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20240319164734_RegionCityWarehouseCreate")]
    partial class RegionCityWarehouseCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("FileTagProduct", b =>
                {
                    b.Property<int>("DescImagesId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("DescImagesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("FileTagProduct");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TinyShop.Areas.Identity.Data.ShopUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TinyShop.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("IdExternal")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Index")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Name")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("RawJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionIdExternal")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<DateTime?>("SoftDeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoftDeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int?>("TheDeliveryTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("TheRegionId")
                        .HasColumnType("int");

                    b.Property<string>("TypeDescription")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("IdExternal");

                    b.HasIndex("RegionIdExternal");

                    b.HasIndex("TheDeliveryTypeId");

                    b.HasIndex("TheRegionId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("TinyShop.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("SoftDeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoftDeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TinyShop.Models.DeliveryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("DeliveryTypes");
                });

            modelBuilder.Entity("TinyShop.Models.FileTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("Body")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Ext")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<long?>("Length")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("FileTags");
                });

            modelBuilder.Entity("TinyShop.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CityId")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime>("OrderDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RegionId")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime?>("SoftDeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoftDeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int?>("TheCustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("TheDeliveryTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("TheOrderStatusId")
                        .HasColumnType("int");

                    b.Property<int?>("ThePaymentTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("WarehouseId")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("TheCustomerId");

                    b.HasIndex("TheDeliveryTypeId");

                    b.HasIndex("TheOrderStatusId");

                    b.HasIndex("ThePaymentTypeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TinyShop.Models.OrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<decimal?>("PriceSnapshot")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SoftDeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoftDeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int?>("TheOrderId")
                        .HasColumnType("int");

                    b.Property<int?>("TheProductId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("TheOrderId");

                    b.HasIndex("TheProductId");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("TinyShop.Models.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("TinyShop.Models.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");
                });

            modelBuilder.Entity("TinyShop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductGroupId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SoftDeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoftDeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("ProductGroupId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TinyShop.Models.ProductGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("SoftDeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoftDeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("ProductGroups");
                });

            modelBuilder.Entity("TinyShop.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("IdExternal")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("RawJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SoftDeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoftDeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int?>("TheDeliveryTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("IdExternal");

                    b.HasIndex("TheDeliveryTypeId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("TinyShop.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CityIdExternal")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("IdExternal")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("RawJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SoftDeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoftDeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int?>("TheCityId")
                        .HasColumnType("int");

                    b.Property<int?>("TheDeliveryTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("TheWarehouseTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("WarehouseTypeIdExternal")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.HasKey("Id");

                    b.HasIndex("CityIdExternal");

                    b.HasIndex("IdExternal");

                    b.HasIndex("TheCityId");

                    b.HasIndex("TheDeliveryTypeId");

                    b.HasIndex("TheWarehouseTypeId");

                    b.HasIndex("WarehouseTypeIdExternal");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("TinyShop.Models.WarehouseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("IdExternal")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("RawJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SoftDeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoftDeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int?>("TheDeliveryTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("IdExternal");

                    b.HasIndex("TheDeliveryTypeId");

                    b.ToTable("WarehouseTypes");
                });

            modelBuilder.Entity("FileTagProduct", b =>
                {
                    b.HasOne("TinyShop.Models.FileTag", null)
                        .WithMany()
                        .HasForeignKey("DescImagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TinyShop.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TinyShop.Areas.Identity.Data.ShopUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TinyShop.Areas.Identity.Data.ShopUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TinyShop.Areas.Identity.Data.ShopUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TinyShop.Areas.Identity.Data.ShopUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TinyShop.Models.City", b =>
                {
                    b.HasOne("TinyShop.Models.DeliveryType", "TheDeliveryType")
                        .WithMany()
                        .HasForeignKey("TheDeliveryTypeId");

                    b.HasOne("TinyShop.Models.Region", "TheRegion")
                        .WithMany()
                        .HasForeignKey("TheRegionId");

                    b.Navigation("TheDeliveryType");

                    b.Navigation("TheRegion");
                });

            modelBuilder.Entity("TinyShop.Models.Order", b =>
                {
                    b.HasOne("TinyShop.Models.Customer", "TheCustomer")
                        .WithMany()
                        .HasForeignKey("TheCustomerId");

                    b.HasOne("TinyShop.Models.DeliveryType", "TheDeliveryType")
                        .WithMany()
                        .HasForeignKey("TheDeliveryTypeId");

                    b.HasOne("TinyShop.Models.OrderStatus", "TheOrderStatus")
                        .WithMany()
                        .HasForeignKey("TheOrderStatusId");

                    b.HasOne("TinyShop.Models.PaymentType", "ThePaymentType")
                        .WithMany()
                        .HasForeignKey("ThePaymentTypeId");

                    b.Navigation("TheCustomer");

                    b.Navigation("TheDeliveryType");

                    b.Navigation("TheOrderStatus");

                    b.Navigation("ThePaymentType");
                });

            modelBuilder.Entity("TinyShop.Models.OrderLine", b =>
                {
                    b.HasOne("TinyShop.Models.Order", "TheOrder")
                        .WithMany("Lines")
                        .HasForeignKey("TheOrderId");

                    b.HasOne("TinyShop.Models.Product", "TheProduct")
                        .WithMany("OrderLines")
                        .HasForeignKey("TheProductId");

                    b.Navigation("TheOrder");

                    b.Navigation("TheProduct");
                });

            modelBuilder.Entity("TinyShop.Models.Product", b =>
                {
                    b.HasOne("TinyShop.Models.ProductGroup", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductGroup");
                });

            modelBuilder.Entity("TinyShop.Models.Region", b =>
                {
                    b.HasOne("TinyShop.Models.DeliveryType", "TheDeliveryType")
                        .WithMany()
                        .HasForeignKey("TheDeliveryTypeId");

                    b.Navigation("TheDeliveryType");
                });

            modelBuilder.Entity("TinyShop.Models.Warehouse", b =>
                {
                    b.HasOne("TinyShop.Models.City", "TheCity")
                        .WithMany()
                        .HasForeignKey("TheCityId");

                    b.HasOne("TinyShop.Models.DeliveryType", "TheDeliveryType")
                        .WithMany()
                        .HasForeignKey("TheDeliveryTypeId");

                    b.HasOne("TinyShop.Models.WarehouseType", "TheWarehouseType")
                        .WithMany()
                        .HasForeignKey("TheWarehouseTypeId");

                    b.Navigation("TheCity");

                    b.Navigation("TheDeliveryType");

                    b.Navigation("TheWarehouseType");
                });

            modelBuilder.Entity("TinyShop.Models.WarehouseType", b =>
                {
                    b.HasOne("TinyShop.Models.DeliveryType", "TheDeliveryType")
                        .WithMany()
                        .HasForeignKey("TheDeliveryTypeId");

                    b.Navigation("TheDeliveryType");
                });

            modelBuilder.Entity("TinyShop.Models.Order", b =>
                {
                    b.Navigation("Lines");
                });

            modelBuilder.Entity("TinyShop.Models.Product", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("TinyShop.Models.ProductGroup", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
