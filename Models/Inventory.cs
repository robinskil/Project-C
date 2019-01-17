using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectC_v2.Models
{
    public class Inventory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int InventoryId { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public Single Price { get; set; }
        [Required]
        [Display(Name = "Product")]
        [ForeignKey("Product")]
        [Remote("ValidatePPCombination" , "Inventories", HttpMethod = "POST", AdditionalFields = "PlatformId" , ErrorMessage = "This combination of Product and Platform already exists.")]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Platform")]
        [ForeignKey("GamePlatform")]
        [Remote("ValidatePPCombination", "Inventories", HttpMethod = "POST", AdditionalFields = "ProductId" , ErrorMessage = "This combination of Product and Platform already exists.")]
        public int PlatformId { get; set; }
        public virtual GamePlatform GamePlatform { get; set; }
        public virtual Product Product { get; set; }
    }
}