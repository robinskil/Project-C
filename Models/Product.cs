using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using System.Security.Policy;

namespace ProjectC_v2.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Cannot be longer than 200 characters")]
        [StringLength(200,MinimumLength = 3 , ErrorMessage = "Minimum length is 3 characters")]
        [RegularExpression(@"[^:>#*]+|([^:>#*][^>#*]+[^:>#*])$")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }
        [Required]
        [Display(Name = "Game Type")]
        public int GameTypeId { get; set; }
        [Required]
        [Display(Name = "PG Rating")]
        public int PGRatingId { get; set; }
        public virtual  Publisher Publisher { get; set; }
        public virtual GameType GameType { get; set; }
        public virtual PGRating PGRating { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }
        public virtual ICollection<ProductVideo> ProductVideos { get; set; }
        public virtual ICollection<Image> ProductImages { get; set; }
    }
}