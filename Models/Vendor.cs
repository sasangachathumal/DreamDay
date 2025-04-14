using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int VendorCategoryId { get; set; }

        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public required string Phone { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Address { get; set; }

        [ForeignKey("VendorCategoryId")]
        public  VendorCategory? VendorCategory { get; set; }
        
        public ICollection<VendorPackage>? VendorPackages { get; set; }
        public ICollection<VendorReviews>? VendorReviews { get; set; }
        public ICollection<VendorImages>? VendorImages { get; set; }
    }
}
