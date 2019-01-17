using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectC_v2.Models;

namespace ProjectC_v2.Models
{
    public class ShoppingCartItem
    {
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int InventoryId { get; set; }
        [Required]
        public int Amount { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual Inventory Inventory { get; set; }

        public ShoppingCartItem(int shoppingCartId, int inventoryId, int amount)
        {
            this.ShoppingCartId = shoppingCartId;
            this.InventoryId = inventoryId;
            this.Amount = amount;
        }
        public ShoppingCartItem(){}
    }
}