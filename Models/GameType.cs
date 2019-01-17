using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectC_v2.Models
{
    public class GameType
    {
        [Key]
        public int GameTypeId { get; set; }
        [Required]
        public string GenreName { get; set; }
    }
}