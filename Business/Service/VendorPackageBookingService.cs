using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;

namespace DreamDay.Business.Service
{
    public class VendorPackageBookingService : IVendorPackageBookingService
    {
        private readonly ApplicationDbContext _context;
        public VendorPackageBookingService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public bool AddVendorPackageBooking(VendorPackageBooking vendorPackageBooking)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVendorPackageBooking(int id)
        {
            throw new NotImplementedException();
        }

        public List<VendorPackageBooking> GetAllVendorPackageBookings()
        {
            throw new NotImplementedException();
        }

        public VendorPackageBooking GetVendorPackageBookingById(int id)
        {
            throw new NotImplementedException();
        }

        public List<VendorPackageBooking> GetVendorPackageBookingsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public List<VendorPackageBooking> GetVendorPackageBookingsByVendorId(int vendorId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVendorPackageBooking(VendorPackageBooking vendorPackageBooking)
        {
            throw new NotImplementedException();
        }
    }
}
