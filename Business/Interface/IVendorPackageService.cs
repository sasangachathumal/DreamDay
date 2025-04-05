using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorPackageService
    {
        List<VendorPackage> GetAllVendorPackages();
        List<VendorPackage> GetVendorPackagesByVendorId(int vendorId);
        VendorPackage GetVendorPackageById(int id);
        bool AddVendorPackage(VendorPackage vendorPackage);
        bool UpdateVendorPackage(VendorPackage vendorPackage);
        bool DeleteVendorPackage(int id);
    }
}
