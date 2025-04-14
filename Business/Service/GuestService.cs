using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamDay.Business.Service
{
    public class GuestService : IGuestService
    {
        private readonly ApplicationDbContext _context;
        public GuestService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public bool AddGuest(Guest guest)
        {
            if (guest == null)
            {
                return false;
            }
            try
            {
                _context.Guests.Add(guest);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error adding guest: {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }

        public bool DeleteGuest(int id)
        {
            if (id == 0)
            {
                return false;
            }
            try
            {
                var guest = _context.Guests
                .Include(c => c.Wedding)
                .FirstOrDefault(m => m.Id == id);
                if (guest == null)
                {
                    return false;
                }
                else
                {
                    _context.Guests.Remove(guest);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error deleting guest (ID: {id}): {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }

        public Guest GetGuestById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            try
            {
                var guest = _context.Guests
                .Include(c => c.Wedding)
                .FirstOrDefault(m => m.Id == id);
                if (guest == null)
                {
                    return null;
                }
                return guest;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error on getting guest: {ex.Message}");
                // Optionally return false or a specific error code/message
                return null;
            }
        }

        public List<Guest> GetGuestsByWeddingId(int weddingId)
        {
            if (weddingId == 0)
            {
                return new List<Guest>();
            }

            try
            {
                var weddingGuests = _context.Guests
                .Where(m => m.WeddingId == weddingId)
                .ToList();

                return weddingGuests;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error retrieving checklist items for wedding (ID: {weddingId}): {ex.Message}");
                // Optionally return an empty list or a specific error code/message
                return new List<Guest>();
            }
        }

        public bool MarkAsAttending(int id)
        {
            if (id == 0)
            {
                return false;
            }

            try
            {
                var guest = _context.Guests.Find(id);
                if (guest == null)
                {
                    return false;
                }
                guest.RSVP = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error marking guest as attending (ID: {id}): {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }

        public bool UpdateGuest(Guest guest)
        {
            if (guest == null)
            {
                return false;
            }
            try
            {
                _context.Update(guest);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error updating guest: {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }
    }
}
