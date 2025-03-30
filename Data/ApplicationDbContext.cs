using DreamDay.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DreamDay.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

        public DbSet<Wedding> Weddings { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<Timeline> Timelines { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<VendorCategory> VendorCategories { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorReviews> VendorReviews { get; set; }
        public DbSet<VendorPackage> VendorPackages { get; set; }
        public DbSet<VendorPackageBooking> VendorPackagesBooking { get; set; }
    }
}
