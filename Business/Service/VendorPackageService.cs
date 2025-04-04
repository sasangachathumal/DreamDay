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
            _context.VendorPackages.Add(vendorPackage);
            return _context.SaveChanges() > 0;
        }

        public List<VendorPackage> GetAllVendorPackages()
        {
            return _context.VendorPackages.ToList();
        }

    }
}
