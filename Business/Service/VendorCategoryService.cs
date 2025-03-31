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
        public Task<bool> AddVendorCategory(VendorCategory vendorCategory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteVendorCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorCategory>> GetAllVendorCategories()
        {
            throw new NotImplementedException();
        }

        public Task<VendorCategory> GetVendorCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateVendorCategory(VendorCategory vendorCategory)
        {
            throw new NotImplementedException();
        }
    }
}
