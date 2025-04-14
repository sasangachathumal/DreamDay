using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorService
    {
        List<Vendor> GetAllVendors();
        Vendor GetVendorById(int id);

        bool AddVendor(Vendor vendor);
        bool UpdateVendor(Vendor vendor);
        bool DeleteVendor(int id);
    }
}
