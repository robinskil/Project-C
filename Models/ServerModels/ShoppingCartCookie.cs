using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Models.ServerModels
{
    public sealed class ShoppingCartCookie
    {
        public List<ProductCookie> Products { get; } = new List<ProductCookie>();

        public ShoppingCartCookie(ProductCookie productCookie)
        {
            Products = new List<ProductCookie>()
            {
                productCookie
            };
        }
        public void AddProduct(ProductCookie productCookie)
        {
            if (productCookie == null)
            {
                throw new ArgumentNullException(nameof(productCookie));
            }
            if (Products.Any(p => p.InventoryId == productCookie.InventoryId))
            {
                var product = Products.FirstOrDefault(p => p.InventoryId == productCookie.InventoryId);
                if (product != null)
                {
                    product.IncreaseAmount();
                }
                else
                {
                    throw new Exception("Something went wrong inside appending a cookie");
                }
            }
            else
            {
                Products.Add(productCookie);
            }
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static ShoppingCartCookie ShoppingCartCookieFromString(string JsonString)
        {
            return JsonConvert.DeserializeObject<ShoppingCartCookie>(JsonString);
        }
    }
}
