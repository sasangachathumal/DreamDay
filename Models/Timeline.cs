using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class Timeline
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int WeddingId { get; set; }

        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public bool IsDone { get; set; } = false;

        [ForeignKey("WeddingId")]
        public required Wedding Wedding { get; set; }
    }
}
