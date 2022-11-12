using Microsoft.EntityFrameworkCore;
using savings_app_backend.Models.Entities;
using savings_app_backend.WebSite.Services;

namespace savings_app_backend.Extention
{
    public static class ModelBuilderExtensions
    {
        public static void SeedProducts(this ModelBuilder modelBuilder,
            DataAccessServiceProducts dataAccessServiceProducts)
        {
            var products = dataAccessServiceProducts.GetProducts();
            modelBuilder.Entity<Product>().HasData((IEnumerable<Product>)products);
            
        }
        public static void SeedUserAuth(this ModelBuilder modelBuilder,
            DataAccessServiceUserAuth dataAccessServiceUserAuth)
        {
            var userAuth = dataAccessServiceUserAuth.GetUserAuths();
            
            modelBuilder.Entity<UserAuth>().HasData((IEnumerable<UserAuth>)userAuth);

        }
        public static void SeedBuyers(this ModelBuilder modelBuilder,
            DataAccessServiceBuyers dataAccessServiceBuyers)
        {
            var buyers = dataAccessServiceBuyers.GetBuyers();

            modelBuilder.Entity<Buyer>().HasData((IEnumerable<Buyer>)buyers);

        }
        public static void SeedRestaurants(this ModelBuilder modelBuilder,
            DataAccessServiceRestaurants dataAccessServiceRestaurants)
        {
            var restaurants = dataAccessServiceRestaurants.GetRestaurants();
            modelBuilder.Entity<Restaurant>().HasData((IEnumerable<Restaurant>)restaurants);
            
        }
        public static void SeedOrders(this ModelBuilder modelBuilder,
            DataAccessServiceOrders dataAccessServiceOrders)
        {
            var orders = dataAccessServiceOrders.GetOrders();
            modelBuilder.Entity<Order>().HasData((IEnumerable<Order>)orders);
            
        }
        public static void SeedPickups(this ModelBuilder modelBuilder,
            DataAccessServicePickups dataAccessServicePickups)
        {
            var pickups = dataAccessServicePickups.GetPickups();
            modelBuilder.Entity<Pickup>().HasData((IEnumerable<Pickup>)pickups);
            
        }
    }
}
