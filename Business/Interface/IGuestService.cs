using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IGuestService
    {
        List<Guest> GetAllGuests();
        List<Guest> GetGuestsByWeddingId(int weddingId);
        List<Guest> GetGuestsOfWeddingByRSVP(int weddingId, bool RSVP);
        List<Guest> GetGuestsOfWeddingByMealPreference(int weddingId, MealPreference mealPreference);
        List<Guest> GetGuestsOfWeddingByTableNumber(int weddingId, int tableNumber);
        Guest GetGuestById(int id);
        bool AddGuest(Guest guest);
        bool UpdateGuest(Guest guest);
        bool DeleteGuest(int id);
        bool MarkAsAttending(int id);
    }
}
