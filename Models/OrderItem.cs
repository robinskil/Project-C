using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectC_v2.Models
{
    public class OrderItem
    {
        [ForeignKey("Order")]
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("Inventory")]
        [Required]
        public int InventoryId { get; set; }
        [Required]
        public int Amount { get; set; }
        public Single PriceBought { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual Order Order { get; set; }
    }
}