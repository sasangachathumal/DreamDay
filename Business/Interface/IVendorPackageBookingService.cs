using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorPackageBookingService
    {
        Task<List<VendorPackageBooking>> GetAllVendorPackageBookings();
        Task<List<VendorPackageBooking>> GetVendorPackageBookingsByVendorId(int vendorId);
        Task<List<VendorPackageBooking>> GetVendorPackageBookingsByUserId(int userId);
        Task<VendorPackageBooking> GetVendorPackageBookingById(int id);
        Task<bool> AddVendorPackageBooking(VendorPackageBooking vendorPackageBooking);
        Task<bool> UpdateVendorPackageBooking(VendorPackageBooking vendorPackageBooking);
        Task<bool> DeleteVendorPackageBooking(int id);
    }
}
