using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamDay.Business.Service
{
    public class VendorService : IVendorService
    {
        private readonly ApplicationDbContext _context;
        public VendorService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public bool AddVendor(Vendor vendor)
        {
            try
            {
                // Ensure the VendorCategoryId exists before saving
                var category = _context.VendorCategories.Find(vendor.VendorCategoryId);
                if (category == null)
                {
                    throw new Exception("Vendor category not found. Cannot add vendor.");
                }

                vendor.VendorCategory = category; // ✅ Manually assign the category

                _context.Vendors.Add(vendor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }





        public bool DeleteVendor(int id)
        {
            throw new NotImplementedException();
        }

        public List<Vendor> GetAllVendors()
        {
            return _context.Vendors.Include(v => v.VendorCategory).ToList();
        }

        public Vendor GetVendorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Vendor> GetVendorsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVendor(Vendor vendor)
        {
            throw new NotImplementedException();
        }
    }
}
