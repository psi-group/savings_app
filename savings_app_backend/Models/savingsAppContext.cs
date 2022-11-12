using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using savings_app_backend.Extention;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.WebSite.Services;

namespace savings_app_backend.Models
{
    public class savingsAppContext : DbContext
    {
        private DataAccessServiceProducts _dataAccessServiceProducts;
        private DataAccessServiceRestaurants _dataAccessServiceRestaurants;
        private DataAccessServiceOrders _dataAccessServiceOrders;
        private DataAccessServicePickups _dataAccessServicePickups;
        private DataAccessServiceUserAuth _dataAccessServiceUserAuth;
        private DataAccessServiceBuyers _dataAccessServiceBuyers;
        public savingsAppContext(DbContextOptions<savingsAppContext> options,
            DataAccessServiceProducts dataAccessServiceProducts,
            DataAccessServiceOrders dataAccessServiceOrders,
            DataAccessServiceRestaurants dataAccessServiceRestaurants,
            DataAccessServicePickups dataAccessServicePickups,
            DataAccessServiceUserAuth dataAccessServiceUserAuth,
            DataAccessServiceBuyers dataAccessServiceBuyers)
            : base(options)
        {
            _dataAccessServiceProducts = dataAccessServiceProducts;
            _dataAccessServiceOrders = dataAccessServiceOrders;
            _dataAccessServicePickups = dataAccessServicePickups;
            _dataAccessServiceRestaurants = dataAccessServiceRestaurants;
            _dataAccessServiceUserAuth = dataAccessServiceUserAuth;
            _dataAccessServiceBuyers = dataAccessServiceBuyers;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
            .HasOne(b => b.UserAuth)
            .WithOne().
            HasForeignKey<Restaurant>(b => b.UserAuthId);

            modelBuilder.Entity<Restaurant>()
                .HasMany(rest => rest.Products)
                .WithOne(prd => prd.Restaurant)
                .HasForeignKey(fk => fk.RestaurantID);


            modelBuilder.Entity<Product>()
                .HasMany(rest => rest.Pickups)
                .WithOne()
                .HasForeignKey(fk => fk.ProductId);


            modelBuilder.Entity<Buyer>()
            .HasOne(b => b.UserAuth)
            .WithOne().
            HasForeignKey<Buyer>(b => b.UserAuthId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Buyer)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.BuyerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Pickup)
                .WithOne()
                .HasForeignKey<Order>(o => o.PickupId);

            modelBuilder.Entity<Restaurant>()
                .HasOne(o => o.Address)
                .WithOne()
                .HasForeignKey<Restaurant>(o => o.AddressId);

            modelBuilder.Entity<Buyer>()
                .HasOne(o => o.Address)
                .WithOne()
                .HasForeignKey<Buyer>(o => o.AddressId);

            /*modelBuilder.SeedUserAuth(_dataAccessServiceUserAuth);
            
            modelBuilder.SeedPickups(_dataAccessServicePickups);
            modelBuilder.SeedRestaurants(_dataAccessServiceRestaurants);
            modelBuilder.SeedProducts(_dataAccessServiceProducts);
            modelBuilder.SeedBuyers(_dataAccessServiceBuyers);
            modelBuilder.SeedOrders(_dataAccessServiceOrders);*/
            
            modelBuilder
                .Entity<Product>()
                .Property(e => e.AmountType)
                .HasConversion(
                    v => v.ToString(),
                    v => (AmountType)Enum.Parse(typeof(AmountType), v));

            modelBuilder
                .Entity<Product>()
                .Property(e => e.Category)
                .HasConversion(
                    v => v.ToString(),
                    v => (Category)Enum.Parse(typeof(Category), v));

            modelBuilder
                .Entity<Order>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));



            modelBuilder
                .Entity<Pickup>()
                .Property(e => e.status)
                .HasConversion(
                    v => v.ToString(),
                    v => (PickupStatus)Enum.Parse(typeof(PickupStatus), v));
        }

        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<Order> Orders { get; set; }

        public DbSet<Pickup> Pickups { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<UserAuth> UserAuths { get; set; }

        public DbSet<Address> Addresses { get; set; }

    }
}
