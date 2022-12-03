using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class SavingsAppContext : DbContext
    {
        
        public SavingsAppContext(DbContextOptions<SavingsAppContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Buyer>().ToTable("Buyers");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurants");

            modelBuilder.Entity<User>().OwnsOne(p => p.Address);
            modelBuilder.Entity<User>().OwnsOne(p => p.UserAuth);

            modelBuilder.Entity<Restaurant>()
                .HasMany(rest => rest.Products)
                .WithOne(prd => prd.Restaurant)
                .HasForeignKey(fk => fk.RestaurantID);

            modelBuilder.Entity<Product>()
                .HasMany(rest => rest.Pickups)
                .WithOne()
                .HasForeignKey(fk => fk.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Buyer)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.BuyerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(order => order.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Pickup)
                .WithOne(pickup => pickup.OrderItem)
                .HasForeignKey<OrderItem>(oi => oi.PickupId);

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
                .Entity<OrderItem>()
                .Property(e => e.OrderItemStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (OrderItemStatus)Enum.Parse(typeof(OrderItemStatus), v));


            modelBuilder
                .Entity<Pickup>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (PickupStatus)Enum.Parse(typeof(PickupStatus), v));
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Pickup> Pickups { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Buyer> Buyers { get; set; }

    }
}
