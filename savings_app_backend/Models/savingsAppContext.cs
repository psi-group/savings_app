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
        public savingsAppContext(DbContextOptions<savingsAppContext> options,
            DataAccessServiceProducts dataAccessServiceProducts,
            DataAccessServiceOrders dataAccessServiceOrders,
            DataAccessServiceRestaurants dataAccessServiceRestaurants,
            DataAccessServicePickups dataAccessServicePickups,
            DataAccessServiceUserAuth dataAccessServiceUserAuth)
            : base(options)
        {
            _dataAccessServiceProducts = dataAccessServiceProducts;
            _dataAccessServiceOrders = dataAccessServiceOrders;
            _dataAccessServicePickups = dataAccessServicePickups;
            _dataAccessServiceRestaurants = dataAccessServiceRestaurants;
            _dataAccessServiceUserAuth = dataAccessServiceUserAuth;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.SeedUserAuth(_dataAccessServiceUserAuth);
            modelBuilder.SeedOrders(_dataAccessServiceOrders);
            modelBuilder.SeedPickups(_dataAccessServicePickups);
            modelBuilder.SeedRestaurants(_dataAccessServiceRestaurants);
            modelBuilder.SeedProducts(_dataAccessServiceProducts);

            modelBuilder.Entity<Restaurant>()
            .HasOne(b => b.UserAuth)
            .WithOne().
            HasForeignKey<Restaurant>(rest => rest.UserAuthId);

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

        public DbSet<Product> Product { get; set; } = default!;

        public DbSet<Order> Order { get; set; }

        public DbSet<Pickup> Pickup { get; set; }

        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Buyer> Buyer { get; set; }
        public DbSet<UserAuth> UserAuth { get; set; }

    }
}
