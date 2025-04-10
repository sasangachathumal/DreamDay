﻿using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorPackageBookingService
    {
        List<VendorPackageBooking> GetAllVendorPackageBookings();
        List<VendorPackageBooking> GetVendorPackageBookingsByVendorId(int vendorId);
        List<VendorPackageBooking> GetVendorPackageBookingsByUserId(int userId);
        VendorPackageBooking GetVendorPackageBookingById(int id);
        bool AddVendorPackageBooking(VendorPackageBooking vendorPackageBooking);
        bool UpdateVendorPackageBooking(VendorPackageBooking vendorPackageBooking);
        bool DeleteVendorPackageBooking(int id);
    }
}
