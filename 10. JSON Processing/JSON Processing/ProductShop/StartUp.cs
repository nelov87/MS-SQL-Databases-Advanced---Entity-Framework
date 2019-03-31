using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using ProductShop.ViewModels;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //var result = ImportUsers(context, File.ReadAllText(@"C:\Users\IvayloNelov\source\repos\MS SQL Databases Advanced - Entity Framework\10. JSON Processing\JSON Processing\ProductShop\Datasets\users.json"));
            //var result = ImportProducts(context, File.ReadAllText(@"C:\Users\IvayloNelov\source\repos\MS SQL Databases Advanced - Entity Framework\10. JSON Processing\JSON Processing\ProductShop\Datasets\products.json"));
            //var result = ImportCategories(context, File.ReadAllText(@"C:\Users\IvayloNelov\source\repos\MS SQL Databases Advanced - Entity Framework\10. JSON Processing\JSON Processing\ProductShop\Datasets\categories.json"));
            //var result = ImportCategoryProducts(context, File.ReadAllText(@"C:\Users\IvayloNelov\source\repos\MS SQL Databases Advanced - Entity Framework\10. JSON Processing\JSON Processing\ProductShop\Datasets\categories-products.json"));

            string result = GetProductsInRange(context);
            
            Console.WriteLine($"{result}");

            



            Console.ReadLine();
            
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            User[] validUsers = JsonConvert.DeserializeObject<User[]>(inputJson).Where(x => x.LastName.Length >=3 || x.LastName != null).ToArray();

            context.AddRange(validUsers);
            context.SaveChanges();

            return $"Successfully imported {validUsers.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            List<Product> validProducts = JsonConvert.DeserializeObject<List<Product>>(inputJson).Where(x => x.Name.Length >= 3 || x.Name != null).ToList();

            context.AddRange(validProducts);
            context.SaveChanges();

            return $"Successfully imported {validProducts.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            List<Category> validCategory = JsonConvert.DeserializeObject<List<Category>>(inputJson).Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.Count() >=3 && x.Name.Count() <= 15).ToList();

            context.AddRange(validCategory);
            context.SaveChanges();

            return $"Successfully imported {validCategory.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            CategoryProduct[] validCatProd = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson).ToArray();

            context.AddRange(validCatProd);
            context.SaveChanges();

            return $"Successfully imported {validCatProd.Length}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {

            var products = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new ProductDto()
                {
                    Name = x.Name,
                    Price = x.Price,
                    Seller = $"{x.Seller.FirstName} {x.Seller.LastName}"
                })
                .OrderBy(x => x.Price)
                .ToList();

            string result = JsonConvert.SerializeObject(products, Formatting.Indented);

            return result;

        }
    }
}