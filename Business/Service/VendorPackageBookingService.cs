using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.EntityFrameworkCore;

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
            if (vendorPackageBooking == null)
            {
                return false;
            }
            try
            {
                _context.VendorPackagesBooking.Add(vendorPackageBooking);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error adding vendorPackageBooking: {ex.Message}");
                // Optionally return false
                return false;
            }
        }

        public bool DeleteVendorPackageBooking(int id)
        {
            if (id == 0)
            {
                return false;
            }
            try
            {
                var vendorPackageBooking = _context.VendorPackagesBooking
                .FirstOrDefault(m => m.Id == id);
                if (vendorPackageBooking == null)
                {
                    return false;
                }
                else
                {
                    _context.VendorPackagesBooking.Remove(vendorPackageBooking);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error deleting vendorPackageBooking (ID: {id}): {ex.Message}");
                // Optionally return false
                return false;
            }
        }

        public VendorPackageBooking GetVendorPackageBookingById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            try
            {
                var vendorPackageBooking = _context.VendorPackagesBooking
                    .Include(c => c.VendorPackage)
                    .Include(c => c.Wedding)
                .FirstOrDefault(m => m.Id == id);
                if (vendorPackageBooking == null)
                {
                    return null;
                }
                return vendorPackageBooking;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error on getting vendorPackageBooking item: {ex.Message}");
                // Optionally return false
                return null;
            }
        }

        public List<VendorPackageBooking> GetVendorPackageBookingsByWeddingId(int weddingId)
        {
            if (weddingId == 0)
            {
                return new List<VendorPackageBooking>();
            }

            try
            {
                var VendorPackagesBookingList = _context.VendorPackagesBooking
                    .Include(c => c.VendorPackage)
                    .Include(c => c.Wedding)
                    .Where(m => m.WeddingID == weddingId)
                    .ToList();

                return VendorPackagesBookingList;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error retrieving vendorPackageBookings for wedding (ID: {weddingId}): {ex.Message}");
                // Optionally return an empty list
                return new List<VendorPackageBooking>();
            }
        }

        public List<VendorPackageBooking> GetVendorPackageBookingsByVendorId(int vendorId)
        {
            if (vendorId == 0)
            {
                return new List<VendorPackageBooking>();
            }

            try
            {
                var VendorPackagesBookingList = _context.VendorPackagesBooking
                    .Include(c => c.VendorPackage)
                    .Include(c => c.Wedding)
                    .Where(m => m.VendorPackage.VendorId == vendorId)
                    .ToList();

                return VendorPackagesBookingList;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error retrieving vendorPackageBookings for vendor (ID: {vendorId}): {ex.Message}");
                // Optionally return an empty list
                return new List<VendorPackageBooking>();
            }
        }

        public bool UpdateVendorPackageBooking(VendorPackageBooking vendorPackageBooking)
        {
            if (vendorPackageBooking == null)
            {
                return false;
            }
            try
            {
                _context.Update(vendorPackageBooking);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error updating vendor Package Booking: {ex.Message}");
                // Optionally return false
                return false;
            }
        }

        public bool ConfirmVendorPackageBooking(int id)
        {
            if (id == 0)
            {
                return false;
            }
            try
            {
                var vendorPackageBooking = _context.VendorPackagesBooking
                .FirstOrDefault(m => m.Id == id);
                if (vendorPackageBooking == null)
                {
                    return false;
                }
                else
                {
                    vendorPackageBooking.IsConfirmed = true;
                    _context.Update(vendorPackageBooking);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error confirming vendorPackageBooking (ID: {id}): {ex.Message}");
                // Optionally return false
                return false;
            }
        }

        public List<VendorPackageBooking> GetAllVendorPackages()
        {
            try
            {
                var VendorPackagesBookingList = _context.VendorPackagesBooking
                    .Include(c => c.VendorPackage)
                    .Include(c => c.VendorPackage.Vendor)
                    .Include(c => c.Wedding)
                    .ToList();

                return VendorPackagesBookingList;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error retrieving vendorPackageBookings : {ex.Message}");
                // Optionally return an empty list
                return new List<VendorPackageBooking>();
            }
        }
    }
}
