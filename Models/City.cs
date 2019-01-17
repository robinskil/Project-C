using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Required]
        [StringLength(200)]
        [MaxLength(200)]
        public string CityName { get; set; }
    }
}
