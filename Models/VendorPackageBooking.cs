using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class VendorPackageBooking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int WeddingID { get; set; }
        [Required]
        public int VendorPackageID { get; set; }
        [Required]
        public DateTime BookDate { get; set; }
        [Required]
        public bool IsConfirmed { get; set; }

        [ForeignKey("WeddingID")]
        public required Wedding Wedding { get; set; }
        [ForeignKey("VendorPackageID")]
        public required VendorPackage VendorPackage { get; set; }
    }
}
