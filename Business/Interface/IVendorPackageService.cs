using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorPackageService
    {
        Task<List<VendorPackage>> GetAllVendorPackages();
        Task<List<VendorPackage>> GetVendorPackagesByVendorId(int vendorId);
        Task<VendorPackage> GetVendorPackageById(int id);
        Task<bool> AddVendorPackage(VendorPackage vendorPackage);
        Task<bool> UpdateVendorPackage(VendorPackage vendorPackage);
        Task<bool> DeleteVendorPackage(int id);
    }
}
