using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class ChecklistItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int WeddingId { get; set; }

        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public bool IsDone  { get; set; } = false;

        [ForeignKey("WeddingId")]
        public required Wedding Wedding { get; set; }
    }
}
