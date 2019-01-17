using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Models
{
    public class WishList
    {
        [Key]
        public int WishListId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<WishListItem> WishListItems { get; set; }
    }
}
