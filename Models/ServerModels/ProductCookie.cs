using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Models.ServerModels
{
    public sealed class ProductCookie
    {
        public int InventoryId { get; set; }
        public int Amount { get; set; } = 1;

        public ProductCookie(int InventoryId)
        {
            this.InventoryId = InventoryId;
        }

        public void IncreaseAmount()
        {
            Amount++;
        }

        public void DecreaseAmount()
        {
            if(Amount > 1) Amount--;
        }
    }

    public sealed class ProductCookieComparer : IEqualityComparer<ProductCookie>
    {
        public bool Equals(ProductCookie x, ProductCookie y)
        {
            return x.InventoryId == y.InventoryId;
        }

        public int GetHashCode(ProductCookie obj)
        {
            return obj.GetHashCode();
        }
    }
}
