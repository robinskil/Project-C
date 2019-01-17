using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectC_v2.Database;
using ProjectC_v2.Models;
using ProjectC_v2.Models.ServerModels;

namespace ProjectC_v2.Services
{
    public static class ShoppingCartService
    {
        public static bool CookieToShoppingCart(string value, out ShoppingCartCookie shoppingCart)
        {
            shoppingCart = ShoppingCartCookie.ShoppingCartCookieFromString(value);
            shoppingCart.Products.RemoveAt(0);
            if (shoppingCart != null) return true;
            return false;
        }
        public static void AddProductToShoppingCart(ShoppingCartCookie shoppingCart, int inventorId)
        {
            shoppingCart.AddProduct(new ProductCookie(inventorId));
        }

        public static string CreateNewShoppingCart(int inventoryId)
        {
            return JsonConvert.SerializeObject(new ShoppingCartCookie(new ProductCookie(inventoryId)));
        }

        public static void DecreaseAmount(int inventoryId, ShoppingCartCookie shoppingCartCookie)
        {
            ProductCookie pc = shoppingCartCookie.Products.FirstOrDefault(p => p.InventoryId == inventoryId);
            if (pc != null)
            {
                pc.DecreaseAmount();
            }
        }

        public static void RemoveProduct(int inventoryId, ShoppingCartCookie shoppingCartCookie)
        {
            shoppingCartCookie.Products.RemoveAll(p => p.InventoryId == inventoryId);
        }
        
        public static async Task<ShoppingCart> CookieToShoppingCartAsync(ShoppingCartCookie shoppingCartCookie, WebshopDbContext db)
        {                                                                                                                                   //To prevend Showing products that arent in stock
            List<Inventory> inventories = await db.Inventory.Where(i => shoppingCartCookie.Products.Any(p => i.InventoryId == p.InventoryId) && i.Stock > 0).Include(i=> i.Product).ThenInclude(i => i.ProductImages).Include(p => p.GamePlatform).ToListAsync();
            List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
            foreach (var inv in inventories)
            {
                int amount = shoppingCartCookie.Products.First(p => p.InventoryId == inv.InventoryId).Amount;
                shoppingCartItems.Add(new ShoppingCartItem()
                {
                    Amount = amount,
                    Inventory = inv,
                    InventoryId = inv.InventoryId,
                });
            }
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.ShoppingCartItems = shoppingCartItems;
            return shoppingCart;
        }

        public static ShoppingCart ShoppingCartWishList(ClaimsPrincipal user, WebshopDbContext db)
        {
            WishList wishList = WishListService.GetWishList(user , db);
            if(wishList == null){
                return null;
            }
            return WishListToShoppingCart(wishList);
        }

        private static ShoppingCart WishListToShoppingCart(WishList wishList){
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.ShoppingCartItems = new List<ShoppingCartItem>();
            foreach(var item in wishList.WishListItems)
            {
                if(item.Inventory.Stock > 0){
                    shoppingCart.ShoppingCartItems.Add(new ShoppingCartItem(){
                        Amount = item.Amount,
                        Inventory = item.Inventory,
                        InventoryId = item.InventoryId
                    });
                }
            }
            return shoppingCart;
        }
    }
}
