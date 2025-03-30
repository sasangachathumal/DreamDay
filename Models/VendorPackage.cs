using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class VendorPackage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int VendorId { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        [ForeignKey("VendorId")]
        public required Vendor Vendor { get; set; }
    }
}
