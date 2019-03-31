using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }

        private string connectionString = "Server=DESKTOP-533LOVH\\SQLEXPRESS;Database=SalesDb;Integrated Security=true";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateCustomerEntity(modelBuilder);
            CreateProductEntity(modelBuilder);
            CreateSaleEntity(modelBuilder);
            CreateStoreEntity(modelBuilder);
        }

        private void CreateStoreEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Store>()
                .HasKey(s => s.StoreId);

            modelBuilder
                .Entity<Store>()
                .Property(s => s.Name)
                .HasMaxLength(80)
                .IsUnicode(true)
                .IsRequired(true);

            
        }

        private void CreateSaleEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Sale>()
                .HasKey(s => s.SaleId);

            modelBuilder
                .Entity<Sale>()
                .Property(s => s.Date)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder
                .Entity<Sale>()
                .HasOne(s => s.Store)
                .WithMany(sl => sl.Sales);
            
        }

        private void CreateProductEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .HasKey(p => p.ProductId);

            modelBuilder
                .Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            modelBuilder
                .Entity<Product>()
                .Property(p => p.Quantity)
                .IsRequired(true);

            modelBuilder
                .Entity<Product>()
                .Property(p => p.Price)
                .IsRequired(true);

            modelBuilder
                .Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(250)
                .IsUnicode(true);

            modelBuilder
                .Entity<Product>()
                .HasMany(s => s.Sales)
                .WithOne(p => p.Product);

        }

        private void CreateCustomerEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder
                .Entity<Customer>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(true);

            modelBuilder
                .Entity<Customer>()
                .Property(c => c.Email)
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired(true);

            modelBuilder
                .Entity<Customer>()
                .HasMany(s => s.Sales)
                .WithOne(c => c.Customer);

        }
    }
}
