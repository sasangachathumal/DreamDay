using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
