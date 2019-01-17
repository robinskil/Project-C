using System.ComponentModel.DataAnnotations;

namespace ProjectC_v2.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Input is too long , max characters is 200")]
        public string PublisherName { get; set; }
    }
}