using ProjectC_v2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectC_v2.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        
        public ShoppingCart(int shoppingCartId , Guid userId)
        {
            this.ShoppingCartId = shoppingCartId;
            this.UserId = userId;
        }

        public ShoppingCart(){}
    }
}