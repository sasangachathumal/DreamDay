using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class VendorReviews
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string UserID { get; set; }
        [Required]
        public int VendorID { get; set; }


        public string? Message { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public DateTime date { get; set; } = DateTime.UtcNow;

        [ForeignKey("VendorID")]
        public required Vendor Vendor { get; set; }
        [ForeignKey("UserID")]
        public required ApplicationUser User { get; set; }
    }
}
