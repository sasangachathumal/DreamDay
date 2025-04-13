using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorPackageBookingService
    {
        List<VendorPackageBooking> GetVendorPackageBookingsByVendorId(int vendorId);
        List<VendorPackageBooking> GetVendorPackageBookingsByWeddingId(int weddingId);
        VendorPackageBooking GetVendorPackageBookingById(int id);
        bool AddVendorPackageBooking(VendorPackageBooking vendorPackageBooking);
        bool UpdateVendorPackageBooking(VendorPackageBooking vendorPackageBooking);
        bool DeleteVendorPackageBooking(int id);
        bool ConfirmVendorPackageBooking(int id);
    }
}
