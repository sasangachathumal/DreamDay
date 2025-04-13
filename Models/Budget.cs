using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int WeddingId { get; set; }

        [Required]
        public BudgetCategory Category { get; set; }
        [Required]
        public decimal AllocatedAmount { get; set; }
        public decimal? SpendAmount { get; set; }

        [ForeignKey("WeddingId")]
        public required Wedding Wedding { get; set; }
    }
}
