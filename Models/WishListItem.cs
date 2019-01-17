using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Models
{
    public class WishListItem
    {
        [ForeignKey("WishList")]
        [Required]
        public int WishListId { get; set; }
        [ForeignKey("Inventory")]
        [Required]
        public int InventoryId { get; set; }
        [Required]
        public int Amount { get; set; }
        public virtual WishList WishList { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
