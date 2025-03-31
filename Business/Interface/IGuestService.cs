using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IGuestService
    {
        Task<List<Guest>> GetAllGuests();
        Task<List<Guest>> GetGuestsByWeddingId(int weddingId);
        Task<List<Guest>> GetGuestsOfWeddingByRSVP(int weddingId, bool RSVP);
        Task<List<Guest>> GetGuestsOfWeddingByMealPreference(int weddingId, MealPreference mealPreference);
        Task<List<Guest>> GetGuestsOfWeddingByTableNumber(int weddingId, int tableNumber);
        Task<Guest> GetGuestByIdAsync(int id);
        Task<bool> AddGuest(Guest guest);
        Task<bool> UpdateGuest(Guest guest);
        Task<bool> DeleteGuest(int id);
    }
}
