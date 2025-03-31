using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;

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
            throw new NotImplementedException();
        }

        public Task<bool> DeleteWedding(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Wedding>> GetAllWeddings()
        {
            throw new NotImplementedException();
        }

        public Task<List<Wedding>> GetWeddingByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task<Wedding> GetWeddingById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Wedding>> GetWeddingByPlannerId(int plannerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateWedding(Wedding wedding)
        {
            throw new NotImplementedException();
        }
    }
}
