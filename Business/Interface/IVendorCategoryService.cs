using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorCategoryService
    {
        Task<List<VendorCategory>> GetAllVendorCategories();
        Task<VendorCategory> GetVendorCategoryById(int id);
        Task<bool> AddVendorCategory(VendorCategory vendorCategory);
        Task<bool> UpdateVendorCategory(VendorCategory vendorCategory);
        Task<bool> DeleteVendorCategory(int id);
    }
}
