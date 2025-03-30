using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class AppImages
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string ImageURL { get; set; }
        [Required]
        public int RelatedId { get; set; }
        [Required]
        public ImageRelateType ImageCategory { get; set; }
    }
}
