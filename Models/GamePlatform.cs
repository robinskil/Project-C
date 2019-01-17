using System.ComponentModel.DataAnnotations;

namespace ProjectC_v2.Models
{
    public class GamePlatform
    {
        [Key]
        public int PlatformId { get; set; }
        [Required]
        [RegularExpression(@"[^:>#*]+|([^:>#*][^>#*]+[^:>#*])$")]
        public string PlatformName { get; set; }
    }
}