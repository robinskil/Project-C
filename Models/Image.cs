using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectC_v2.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string URL { get; set; }
        [Required]
        [ForeignKey("Product")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}