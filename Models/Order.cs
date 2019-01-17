using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectC_v2.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [ForeignKey("User")]
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey("OrderStatus")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int StatusId { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Maximum 200 characters")]
        public string Street { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "A postal code contains 6 characters")]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual User User { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual TransactionInfo TransactionInfo { get; set; }
    }
}