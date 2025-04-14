using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamDay.Business.Service
{
    public class VendorImageService : IVendorImageService
    {
        private readonly ApplicationDbContext _context;
        public VendorImageService(ApplicationDbContext dbContext) 
        {
            _context = dbContext;
        }

        public bool AddAppImage(VendorImages vendorImage)
        {
            if (vendorImage == null)
            {
                return false;
            }
            try
            {
                _context.VendorImages.Add(vendorImage);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error adding vendor image: {ex.Message}");
                // Optionally return false
                return false;
            }
        }

        public bool DeleteAppImage(int id)
        {
            if (id == 0)
            {
                return false;
            }
            try
            {
                var vendorImage = _context.VendorImages
                .FirstOrDefault(m => m.Id == id);
                if (vendorImage == null)
                {
                    return false;
                }
                else
                {
                    _context.VendorImages.Remove(vendorImage);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error deleting vendor image (ID: {id}): {ex.Message}");
                // Optionally return false
                return false;
            }
        }

        public VendorImages GetAppImageById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            try
            {
                var vendorImage = _context.VendorImages
                .FirstOrDefault(m => m.Id == id);
                if (vendorImage == null)
                {
                    return null;
                }
                return vendorImage;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error on getting vendor image: {ex.Message}");
                // Optionally return false
                return null;
            }
        }

        public List<VendorImages> GetAppImagesByVendorID(int VebdorId)
        {
            if (VebdorId == 0)
            {
                return new List<VendorImages>();
            }
            try
            {
                var vendorImage = _context.VendorImages
                .Where(m => m.VendorId == VebdorId)
                .ToList();
                return vendorImage;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error retrieving vendor image: {ex.Message}");
                // Optionally return false
                return new List<VendorImages>();
            }
        }
    }
}
