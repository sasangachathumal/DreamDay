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

        public bool DeleteWedding(int id)
        {
            throw new NotImplementedException();
        }

        public List<Wedding> GetAllWeddings()
        {
            throw new NotImplementedException();
        }

        public List<Wedding> GetWeddingByClientId(string clientId)
        {
            if (clientId == null)
            {
                return new List<Wedding>();
            }

            var weddings = _context.Weddings
                .Include(w => w.Client)
                .Include(w => w.Planner)
                .Where(m => m.ClientId == clientId)
                .ToList();

            return weddings;
        }

        public Wedding GetWeddingById(int id)
        {
            throw new NotImplementedException();
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
