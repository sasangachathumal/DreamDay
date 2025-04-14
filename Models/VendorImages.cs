using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class VendorImages
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int VendorId { get; set; }
        [Required]
        public required string ImageURL { get; set; }

        [ForeignKey("VendorId")]
        public Vendor? Vendor { get; set; }
    }
}
