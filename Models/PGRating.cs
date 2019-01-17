using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectC_v2.Models
{
    public class PGRating
    {
        [Key]
        public int PGRatingId { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}