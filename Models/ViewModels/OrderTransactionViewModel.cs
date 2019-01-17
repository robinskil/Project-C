using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Models.ViewModels
{
    public class OrderTransactionViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionInfoId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(200)]
        [StringLength(200, ErrorMessage = "Maximum 200 characters")]
        public string Street { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MinLength(6)]
        [MaxLength(6)]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "A postal code contains 6 characters")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(200)]
        [StringLength(200,ErrorMessage = "Maximum 200 characters")]
        public string City { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(18,MinimumLength = 18 , ErrorMessage = "Contains 18 characters")]
        [MaxLength(18)]
        [MinLength(18)]
        public string IBAN { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email{get;set;}
    }
}
