using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IGuestService
    {
        List<Guest> GetGuestsByWeddingId(int weddingId);
        Guest GetGuestById(int id);
        bool AddGuest(Guest guest);
        bool UpdateGuest(Guest guest);
        bool DeleteGuest(int id);
        bool MarkAsAttending(int id);
    }
}
