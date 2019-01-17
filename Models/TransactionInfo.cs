using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Models
{
    public class TransactionInfo
    {
        [Key]
        public int TransactionInfoId { get; set; }
        [MaxLength(18)]
        [Required]
        public string IBAN { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public int OrderId { get; set; }
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
    }
}
