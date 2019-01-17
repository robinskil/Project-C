using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectC_v2.Models
{
    public class ProductFeature
    {
        [Key]
        public int FeatureId { get; set; }
        [Required]
        [MaxLength(200)]
        [StringLength(200)]
        public string Feature { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}