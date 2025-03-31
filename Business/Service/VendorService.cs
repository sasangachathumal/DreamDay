using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;

namespace DreamDay.Business.Service
{
    public class VendorService : IVendorService
    {
        private readonly ApplicationDbContext _context;
        public VendorService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public Task<bool> AddVendor(Vendor vendor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteVendor(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Vendor>> GetAllVendors()
        {
            throw new NotImplementedException();
        }

        public Task<Vendor> GetVendorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Vendor>> GetVendorsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateVendor(Vendor vendor)
        {
            throw new NotImplementedException();
        }
    }
}
