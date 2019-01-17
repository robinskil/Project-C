using Microsoft.EntityFrameworkCore;
using ProjectC_v2.Database;
using ProjectC_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Services
{
    public static class InventoriesService
    {
        public static async Task<bool> RecordExists(int productId , int platformId , WebshopDbContext db)
        {
            return await db.Inventory.AnyAsync(i => i.ProductId == productId && i.PlatformId == platformId);
        }

        public static async Task<bool> ValidEditAsync(Inventory inventory, WebshopDbContext db)
        {
            return await db.Inventory.AnyAsync(i => i.InventoryId == inventory.InventoryId && i.PlatformId == inventory.PlatformId && i.ProductId == inventory.ProductId);
        }
    }
}
