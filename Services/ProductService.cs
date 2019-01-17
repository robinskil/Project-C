using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectC_v2.Database;
using ProjectC_v2.Helpers;
using ProjectC_v2.Models;

namespace ProjectC_v2.Services
{
    public static class ProductService{

        public static List<Product> GetProducts(WebshopDbContext db){
            return db.Product.Include(inventory => inventory.Inventories)
                    .ThenInclude(platform => platform.GamePlatform)
                    .Include(images => images.ProductImages)
                    .Include(features => features.ProductFeatures)
                    .Include(publisher => publisher.Publisher)
                    .Where(product => product.Inventories.Any(inventory => inventory.ProductId == product.ProductId))
                    .ToList();
        }
        public static List<Product> GetFilteredProducts(string answer, IQueryable<Product> products)
        {
            var productList = new List<Product>();
            foreach (var product in products.ToList())
            {
                if (product.Inventories.Any())
                {
                    foreach (var product1Inventory in product.Inventories)
                    {
                        if (product1Inventory.GamePlatform.PlatformName == answer)
                        {
                            productList.Add(product);
                        }
                    }
                }
            }
            return productList.ToList();
        }

        public static dynamic DynamicPlatforms(List<Product> products)
        {
            List<GamePlatform> gamePlatforms = new List<GamePlatform>();
            foreach(Product p in products){
                foreach(Inventory inv in p.Inventories){
                    if(!gamePlatforms.Exists(gp => inv.GamePlatform.PlatformName == gp.PlatformName)){
                        gamePlatforms.Add(inv.GamePlatform);
                    }
                }
            }
            return gamePlatforms;
        }

        public static dynamic DynamicGameTypes(List<Product> products)
        {
            List<GameType> gameTypes = new List<GameType>();
            foreach(Product p in products){
                if(!gameTypes.Exists(pub => p.GameType.GameTypeId == pub.GameTypeId)){
                    gameTypes.Add(p.GameType);
                }
            }
            return gameTypes;
        }

        public static List<Publisher> DynamicPublishers(List<Product> products)
        {
            List<Publisher> publishers = new List<Publisher>();
            foreach(Product p in products){
                if(!publishers.Exists(pub => p.Publisher.PublisherName == pub.PublisherName)){
                    publishers.Add(p.Publisher);
                }
            }
            return publishers;
        }

        /// <summary>
        /// TODO: Implement stock checking by using cookie reference amount inside user.
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task<bool> CheckExistingInventory(int inventoryId , WebshopDbContext db)
        {
            return await db.Inventory.AnyAsync(i => i.InventoryId == inventoryId);
        }

        public static async Task<List<Product>> GetSearchProducts(string query , WebshopDbContext db)
        {
            var products = await db.Product.Include(p => p.GameType).Include(p => p.PGRating).Include(p => p.Publisher)
                                            .Include(p => p.ProductImages).Include(p => p.Inventories).ThenInclude(i => i.GamePlatform)
                                            .Include(p => p.ProductFeatures)
                                            .Where(
                                                    p => p.Name.Contains(query) ||
                                                    p.GameType.GenreName.Contains(query) ||
                                                    p.ProductFeatures.Any(f => f.Feature.Contains(query)) ||
                                                    p.Publisher.PublisherName.Contains(query) ||
                                                    p.Inventories.Any(i => i.GamePlatform.PlatformName.Contains(query)) ||
                                                    p.Description.Contains(query)
                                            ).ToListAsync();
            products.RemoveAll(i => i.Inventories.Count < 1);
            return SelectiveSearchList(query , products).ToList();
        }
        /// <summary>
        /// Returns a hashset of products based on how relevant they are to the search query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        private static HashSet<Product> SelectiveSearchList(string query , List<Product> products)
        {
            var filteredList = new HashSet<Product>();
            foreach (var item in products)
            {
                if (item.Name.ToLower().Contains(query)) filteredList.Add(item);
            };
            foreach (var item in products)
            {
                if (item.GameType.GenreName.ToLower().Contains(query)) filteredList.Add(item);
            };
            foreach (var item in products)
            {
                if (item.Publisher.PublisherName.ToLower().Contains(query)) filteredList.Add(item);
            };
            foreach (var item in products)
            {
                if (item.ProductFeatures.Any(f => f.Feature.ToLower().Contains(query))) filteredList.Add(item);
            };
            foreach (var item in products)
            {
                if (item.Inventories != null && item.Inventories.Any(i => i.GamePlatform.PlatformName.ToLower().Contains(query))) filteredList.Add(item);
            };
            foreach (var item in products)
            {
                if (item.Description.ToLower().Contains(query)) filteredList.Add(item);
            };
            filteredList.RemoveWhere(p => p.Inventories == null || p.Inventories.Count < 1);
            return filteredList;
        }
    }
}