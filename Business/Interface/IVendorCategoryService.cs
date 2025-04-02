using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorCategoryService
    {
        List<VendorCategory> GetAllVendorCategories();
        VendorCategory GetVendorCategoryById(int id);
        bool AddVendorCategory(VendorCategory vendorCategory);
        bool UpdateVendorCategory(VendorCategory vendorCategory);
        bool DeleteVendorCategory(int id);
    }
}
