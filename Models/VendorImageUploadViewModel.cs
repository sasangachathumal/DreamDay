using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class VendorImageUploadViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Select the image")]
        public IFormFile ImageFile { get; set; }

        public string? ImageUrl { get; set; } // This will be saved in DB
    }
}
