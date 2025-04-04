using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;

namespace DreamDay.Business.Service
{
    public class VendorCategoryService : IVendorCategoryService
    {
        private readonly ApplicationDbContext _context;

        public VendorCategoryService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        // Fetch all vendor categories
        public List<VendorCategory> GetAllVendorCategories()
        {
            return _context.VendorCategories.ToList();
        }

        //Fetch Edit Page to ID Through VendorCategory
        public VendorCategory GetVendorCategoryById(int id)
        {
            return _context.VendorCategories.FirstOrDefault(vc => vc.Id == id);
        }

        // Add New Category
        public bool AddVendorCategory(VendorCategory vendorCategory)
        {
            try
            {
                _context.VendorCategories.Add(vendorCategory);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update category
        public bool UpdateVendorCategory(VendorCategory vendorCategory)
        {
            try
            {
                _context.VendorCategories.Update(vendorCategory);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Delete category
        public bool DeleteVendorCategory(int id)
        {
            try
            {
                var category = _context.VendorCategories.Find(id);
                if (category != null)
                {
                    _context.VendorCategories.Remove(category);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
