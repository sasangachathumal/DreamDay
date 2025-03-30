using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class VendorCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
    }
}
