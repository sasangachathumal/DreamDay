using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;

namespace DreamDay.Business.Service
{
    public class GuestService : IGuestService
    {
        private readonly ApplicationDbContext _context;
        public GuestService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public Task<bool> AddGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGuest(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Guest>> GetAllGuests()
        {
            throw new NotImplementedException();
        }

        public Task<Guest> GetGuestByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Guest>> GetGuestsByWeddingId(int weddingId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Guest>> GetGuestsOfWeddingByMealPreference(int weddingId, MealPreference mealPreference)
        {
            throw new NotImplementedException();
        }

        public Task<List<Guest>> GetGuestsOfWeddingByRSVP(int weddingId, bool RSVP)
        {
            throw new NotImplementedException();
        }

        public Task<List<Guest>> GetGuestsOfWeddingByTableNumber(int weddingId, int tableNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateGuest(Guest guest)
        {
            throw new NotImplementedException();
        }
    }
}
