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
        public bool AddVendorPackage(VendorPackage vendorPackage)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVendorPackage(int id)
        {
            throw new NotImplementedException();
        }

        public List<VendorPackage> GetAllVendorPackages()
        {
            throw new NotImplementedException();
        }

        public VendorPackage GetVendorPackageById(int id)
        {
            throw new NotImplementedException();
        }

        public List<VendorPackage> GetVendorPackagesByVendorId(int vendorId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVendorPackage(VendorPackage vendorPackage)
        {
            throw new NotImplementedException();
        }
    }
}
