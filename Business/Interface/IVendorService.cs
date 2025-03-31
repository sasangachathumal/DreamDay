using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorService
    {
        Task<List<Vendor>> GetAllVendors();
        Task<List<Vendor>> GetVendorsByCategoryId(int categoryId);
        Task<Vendor> GetVendorByIdAsync(int id);
        Task<bool> AddVendor(Vendor vendor);
        Task<bool> UpdateVendor(Vendor vendor);
        Task<bool> DeleteVendor(int id);
    }
}
