using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;

namespace DreamDay.Business.Service
{
    public class VendorPackageService : IVendorPackageService
    {
        private readonly ApplicationDbContext _context;
        public VendorPackageService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public Task<bool> AddVendorPackage(VendorPackage vendorPackage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteVendorPackage(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorPackage>> GetAllVendorPackages()
        {
            throw new NotImplementedException();
        }

        public Task<VendorPackage> GetVendorPackageById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorPackage>> GetVendorPackagesByVendorId(int vendorId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateVendorPackage(VendorPackage vendorPackage)
        {
            throw new NotImplementedException();
        }
    }
}
