using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamDay.Business.Service
{
    public class WeddingService : IWeddingService
    {
        private readonly ApplicationDbContext _context;
        public WeddingService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public bool AddWedding(Wedding wedding)
        {
            _context.Add(wedding);
            int result = _context.SaveChanges();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AssignPlanner(string plannerId, int weddingId)
        {
            if (weddingId == 0)
            {
                return false;
            }
            try
            {
                var wedding = _context.Weddings
                .FirstOrDefault(m => m.Id == weddingId);
                if (wedding == null)
                {
                    return false;
                }
                wedding.PlannerId = plannerId;
                _context.Update(wedding);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error on getting wedding item: {ex.Message}");
                // Optionally return false
                return false;
            }
        }

        public List<Wedding> GetAllWeddings()
        {
            return _context.Weddings
                .Include(w => w.Client)   // Include related client
                .Include(w => w.Planner)  // Include related planner
                .ToList();
        }

        public Wedding GetWeddingByClientId(string clientId)
        {
            if (clientId == null)
            {
                return null;
            }

            var wedding = _context.Weddings
                .Include(w => w.Client)
                .Include(w => w.Planner)
                .FirstOrDefault(m => m.ClientId == clientId);
            if (wedding == null)
            {
                return null;
            }
            else
            {
                return wedding;
            }
        }

        public Wedding GetWeddingById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            try
            {
                var wedding = _context.Weddings
                    .Include(w => w.Client)
                .Include(w => w.Planner)
                .Include(w => w.ChecklistItems)
                .Include(w => w.Guests)
                .Include(w => w.Timelines)
                .Include(w => w.Budgets)
                .Include(w => w.VendorPackageBookings)
                .FirstOrDefault(m => m.Id == id);
                if (wedding == null)
                {
                    return null;
                }
                return wedding;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error on getting wedding item: {ex.Message}");
                // Optionally return false
                return null;
            }
        }

        public List<Wedding> GetWeddingByPlannerId(string plannerId)
        {
            if (plannerId == null)
            {
                return new List<Wedding>();
            }

            var weddings = _context.Weddings
                .Include(w => w.Client)
                .Include(w => w.Planner)
                .Where(m => m.PlannerId == plannerId)
                .ToList();

            return weddings;
        }

        public bool UpdateWedding(Wedding wedding)
        {
            throw new NotImplementedException();
            
        }
    }
}
