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
        public Task<bool> AddVendorPackageBooking(VendorPackageBooking vendorPackageBooking)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteVendorPackageBooking(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorPackageBooking>> GetAllVendorPackageBookings()
        {
            throw new NotImplementedException();
        }

        public Task<VendorPackageBooking> GetVendorPackageBookingById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorPackageBooking>> GetVendorPackageBookingsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorPackageBooking>> GetVendorPackageBookingsByVendorId(int vendorId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateVendorPackageBooking(VendorPackageBooking vendorPackageBooking)
        {
            throw new NotImplementedException();
        }
    }
}
