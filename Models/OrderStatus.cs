using System.ComponentModel.DataAnnotations;

namespace ProjectC_v2.Models
{
    public class OrderStatus
    {
        [Key] public int StatusId { get; set; }
        [Required] public string StatusName { get; set; }
    }
}