using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int WeddingId { get; set; }

        [Required]
        public required string FullName { get; set; }
        [Required]
        public required string Phone { get; set; }
        [Required]
        public bool RSVP { get; set; } = false;
        public MealPreference MealPreference { get; set; } = MealPreference.NonVegetarian;
        public int? TableNumber { get; set; }

        [ForeignKey("WeddingId")]
        public required Wedding Wedding { get; set; }
    }
}
