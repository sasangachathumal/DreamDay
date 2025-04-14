using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamDay.Business.Service
{
    public class VendorReviewService : IVendorReviewService
    {
        private readonly ApplicationDbContext _context;
        public VendorReviewService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public bool AddVendorReview(VendorReviews vendorReview)
        {
            if (vendorReview == null)
            {
                return false;
            }
            try
            {
                _context.VendorReviews.Add(vendorReview);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error Vendor Reviews budget item: {ex.Message}");
                // Optionally return false
                return false;
            }
        }

        public bool DeleteVendorReview(int id)
        {
            if (id == 0)
            {
                return false;
            }
            try
            {
                var vendorReview = _context.VendorReviews
                .FirstOrDefault(m => m.Id == id);
                if (vendorReview == null)
                {
                    return false;
                }
                else
                {
                    _context.VendorReviews.Remove(vendorReview);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error deleting vendor Review (ID: {id}): {ex.Message}");
                // Optionally return false
                return false;
            }
        }

        public List<VendorReviews> GeAlltVendorReviews()
        {
            try
            {
                return _context.VendorReviews
                .Include(p => p.Vendor)
                .Include(v => v.User)
                .ToList();
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error retrieving reviews");
                // Optionally return an empty list or a specific error code/message
                return new List<VendorReviews>();
            }
        }

        public List<VendorReviews> GetVendorReviewsById(int id)
        {
            if (id == 0)
            {
                return new List<VendorReviews>();
            }

            try
            {
                return _context.VendorReviews
                .Include(p => p.Vendor)
                .Where(m => m.Id == id)
                .ToList();
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error retrieving reviews for vendor : {ex.Message}");
                // Optionally return an empty list
                return new List<VendorReviews>();
            }
        }

        public List<VendorReviews> GetVendorReviewsByVendorId(int vendorId)
        {
            if (vendorId == 0)
            {
                return new List<VendorReviews>();
            }

            try
            {
                return _context.VendorReviews
                .Include(p => p.Vendor)
                .Where(m => m.VendorID == vendorId)
                .ToList();
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error retrieving reviews for vendor (ID: {vendorId}): {ex.Message}");
                // Optionally return an empty list or a specific error code/message
                return new List<VendorReviews>();
            }
        }
    }
}
