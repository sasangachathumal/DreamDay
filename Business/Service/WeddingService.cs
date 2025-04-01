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
        public Task<bool> AddWedding(Wedding wedding)
        {
            _context.Add(wedding);
            int result = _context.SaveChanges();

            if (result > 0)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> DeleteWedding(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Wedding>> GetAllWeddings()
        {
            throw new NotImplementedException();
        }

        public Task<List<Wedding>> GetWeddingByClientId(string clientId)
        {
            if (clientId == null)
            {
                return Task.FromResult(new List<Wedding>());
            }

            var weddings = _context.Weddings
                .Include(w => w.Client)
                .Include(w => w.Planner)
                .Where(m => m.ClientId == clientId)
                .ToList();

            return Task.FromResult(weddings);
        }

        public Task<Wedding> GetWeddingById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Wedding>> GetWeddingByPlannerId(string plannerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateWedding(Wedding wedding)
        {
            throw new NotImplementedException();
        }
    }
}
