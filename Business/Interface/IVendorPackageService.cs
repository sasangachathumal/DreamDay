using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorPackageService
    {
        bool AddVendorPackage(VendorPackage vendorPackage);
        List<VendorPackage> GetAllVendorPackages();
    }
}
