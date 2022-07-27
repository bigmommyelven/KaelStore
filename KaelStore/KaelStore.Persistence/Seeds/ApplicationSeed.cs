using KaelStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KaelStore.Persistence.Seeds
{
    public static class ApplicationSeed
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new List<Customer>()
                {
                    new Customer
                    {
                        Id = 1,
                        CustomerName = "Hafizhan Al Wafi",
                        ContactName = "hafiz",
                        ContactTitle = "hafiz",
                        Address = "Jalan in aja dulu",
                        City = "Malang",
                        Country = "Indonesia",
                        Region = "East Java",
                        Fax = "0000",
                        Phone = "0000",
                        PostalCode = "65125"
                    }
                });

            modelBuilder.Entity<Category>().HasData(
                new List<Category>()
                {
                    new Category
                    {
                        Id = 1,
                        CategoryName = "ATK",
                        Description = "Alat tulis kantor"
                    },
                    new Category
                    {
                        Id = 2,
                        CategoryName = "Buku Tulis",
                        Description = "Buku Tulis"
                    }
                });

            modelBuilder.Entity<Product>().HasData(
                new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        ProductName = "Pensil 2B",
                        CategoryId = 1,
                        Price = 3000,
                    },
                    new Product
                    {
                        Id = 2,
                        ProductName = "Buku Tulis Sinar",
                        CategoryId = 2,
                        Price = 5000
                    }
                });

            modelBuilder.Entity<ProductStock>().HasData(
                new List<ProductStock>()
                {
                    new ProductStock
                    {
                        ProductId = 1,
                        Stock = 100
                    },
                    new ProductStock
                    {
                        ProductId = 2,
                        Stock = 100
                    },
                });
        }
    }
}
