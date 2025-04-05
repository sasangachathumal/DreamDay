using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorService
    {
        List<Vendor> GetAllVendors();
        List<Vendor> GetVendorsByCategoryId(int categoryId);
        Vendor GetVendorByIdAsync(int id);
        bool AddVendor(Vendor vendor);
        bool UpdateVendor(Vendor vendor);
        bool DeleteVendor(int id);
    }
}
