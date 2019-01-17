using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Models.ViewModels
{
    public sealed class ChangeNameViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(30)]
        [Required]
        public string Surname { get; set; }
    }
}
