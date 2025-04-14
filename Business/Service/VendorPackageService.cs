using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.VendorPackages
                .Include(p => p.Vendor) // Include Vendor name
                .ToList();
        }

        public bool DeleteVendorPackage(int id)
        {
            var package = _context.VendorPackages.Find(id);
            if (package == null) return false;

            _context.VendorPackages.Remove(package);
            return _context.SaveChanges() > 0;
        }

        public VendorPackage? GetVendorPackageById(int id)
        {
            return _context.VendorPackages
                .Include(p => p.Vendor)
                .FirstOrDefault(p => p.Id == id);
        }

        public bool UpdateVendorPackage(VendorPackage vendorPackage)
        {
            _context.VendorPackages.Update(vendorPackage);
            return _context.SaveChanges() > 0;
        }

        public List<VendorPackage> GetVendorPackagesByVendorId(int id)
        {
            return _context.VendorPackages
                .Include(p => p.Vendor)
                .Where(m => m.VendorId == id)
                .ToList();
        }
    }
}
