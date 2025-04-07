using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorCategoryService
    {
        List<VendorCategory> GetAllVendorCategories(); //  Fetch all vendor categories
        VendorCategory GetVendorCategoryById(int id); //  Get a single category
        bool AddVendorCategory(VendorCategory vendorCategory); // Add 
        bool UpdateVendorCategory(VendorCategory vendorCategory); //  Update category
        bool DeleteVendorCategory(int id); // Delete category
    }
}
