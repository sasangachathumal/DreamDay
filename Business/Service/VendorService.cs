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
        public bool AddVendor(Vendor vendor)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVendor(int id)
        {
            throw new NotImplementedException();
        }

        public List<Vendor> GetAllVendors()
        {
            throw new NotImplementedException();
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
