using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Challenge
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne<Product>(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.ProductId, oi.OrderId } );

            modelBuilder.Entity<Order>()
                .HasOne<Customer>(o => o.Customer)
                .WithOne(c => c.Order)
                .HasForeignKey<Order>(o => o.CustomerId);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}