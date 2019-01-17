using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectC_v2.Database;
using ProjectC_v2.Models;

namespace ProjectC_v2.Services
{
    public static class WishListService
    {
        public static WishList GetWishList(ClaimsPrincipal user, WebshopDbContext db)
        {
            string guidString = user.Claims.Where(claim => claim.Type == ClaimTypes.Sid).Select(s => s.Value).SingleOrDefault();
            if (!Guid.TryParse(guidString, out Guid userId))
            {
                return null;
            }
            return db.WishList.Include(wi => wi.WishListItems).ThenInclude(wi => wi.Inventory).ThenInclude(wi => wi.GamePlatform)
                        .Include(wi => wi.WishListItems).ThenInclude(wi => wi.Inventory).ThenInclude(wi => wi.Product).ThenInclude(p => p.ProductImages).
                        FirstOrDefault(u => u.UserId == userId);
        }

        public static bool AddInventory(int inventoryId, ClaimsPrincipal user, WebshopDbContext db)
        {
            //TODO: Check if theres a wishlist for that user. Else create a new one
            //TODO: Update wishlist item if there is one , else create a new one
            string guidString = user.Claims.Where(claim => claim.Type == ClaimTypes.Sid).Select(s => s.Value).SingleOrDefault();
            if (!Guid.TryParse(guidString, out Guid userId))
            {
                return false;
            }
            WishList wishList = db.WishList.Include(wi => wi.WishListItems).FirstOrDefault(w => w.UserId == userId) ?? CreateWishList(userId, db);
            if (wishList == null) return false;
            if (wishList.WishListItems == null || wishList.WishListItems.Count <= 0)
            {
                db.WishListItem.Add(NewWishListItem(wishList.WishListId, inventoryId));
            }
            else
            {
                WishListItem wishListItem = wishList.WishListItems.FirstOrDefault(i =>
                    i.InventoryId == inventoryId && i.WishListId == wishList.WishListId);
                if (wishListItem != null) wishListItem.Amount++;
                else
                {
                    db.WishListItem.Add(NewWishListItem(wishList.WishListId, inventoryId));
                }
            }
            db.SaveChanges();
            return true;
        }

        public static bool RemoveInventory(int inventoryId, ClaimsPrincipal user, WebshopDbContext db)
        {
            string guidString = user.Claims.Where(claim => claim.Type == ClaimTypes.Sid).Select(s => s.Value).SingleOrDefault();
            if (!Guid.TryParse(guidString, out Guid userId))
            {
                return false;
            }
            WishList wishList = db.WishList.Include(wi => wi.WishListItems).FirstOrDefault(w => w.UserId == userId);
            if (wishList == null)
            {
                return false;
            }
            WishListItem toBeRemoved = db.WishListItem.FirstOrDefault(wi => wi.InventoryId == inventoryId && wishList.WishListId == wi.WishListId);
            if (toBeRemoved == null)
            {
                return false;
            }
            wishList.WishListItems.Remove(toBeRemoved);
            db.SaveChanges();
            return true;
        }

        public static bool DecreaseAmountInventory(int inventoryId, ClaimsPrincipal user, WebshopDbContext db)
        {
            string guidString = user.Claims.Where(claim => claim.Type == ClaimTypes.Sid).Select(s => s.Value).SingleOrDefault();
            if (!Guid.TryParse(guidString, out Guid userId))
            {
                return false;
            }
            WishList wishList = db.WishList.Include(wi => wi.WishListItems).FirstOrDefault(w => w.UserId == userId);
            if (wishList == null)
            {
                return false;
            }
            WishListItem item = wishList.WishListItems.FirstOrDefault(i => i.InventoryId == inventoryId);
            if(item == null || item.Amount < 2){
                return false;
            }
            item.Amount--;
            db.SaveChanges();
            return true;
        }

        public static void RemoveWishList(ClaimsPrincipal user, WebshopDbContext db)
        {

            db.Remove(GetWishList(user, db));
            db.SaveChanges();
        }

        private static WishListItem NewWishListItem(int wishListId, int inventoryId)
        {
            return new WishListItem()
            {
                InventoryId = inventoryId,
                Amount = 1,
                WishListId = wishListId
            };
        }

        private static WishList CreateWishList(Guid userId, WebshopDbContext db)
        {
            // return new WishList(){
            //     Name="Default",
            //     UserId = userId,
            // };
            db.WishList.Add(new WishList()
            {
                Name = "Default",
                UserId = userId,
            });
            db.SaveChanges();
            return db.WishList.FirstOrDefault(w => w.UserId == userId);
        }
    }
}
