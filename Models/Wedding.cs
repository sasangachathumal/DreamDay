using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class Wedding
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string ClientId { get; set; }
        [Required]
        public string? PlannerId { get; set; }


        [Required]
        public DateTime WeddingDate { get; set; }
        public int TotalGuests { get; set; }
        public double FullBudget { get; set; }

        [ForeignKey("ClientId")]
        public required ApplicationUser Client { get; set; }

        [ForeignKey("PlannerId")]
        public ApplicationUser? Planner { get; set; }

        public ICollection<Guest>? Guests { get; set; }
        public ICollection<ChecklistItem>? ChecklistItems { get; set; }
        public ICollection<Timeline>? Timelines { get; set; }
        public ICollection<Budget>? Budgets { get; set; }
        public ICollection<VendorPackageBooking>? VendorPackageBookings { get; set; }

    }
}
