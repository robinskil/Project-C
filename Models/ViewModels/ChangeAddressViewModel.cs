using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Models.ViewModels
{
    public sealed class ChangeAddressViewModel
    {
        [Required]
        [MaxLength(200)]
        public string Street { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        public string PostalCode { get; set; }
    }
}
