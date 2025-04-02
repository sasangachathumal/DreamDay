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
        public bool AddWedding(Wedding wedding)
        {
            throw new NotImplementedException();
        }

        public bool DeleteWedding(int id)
        {
            throw new NotImplementedException();
        }

        public List<Wedding> GetAllWeddings()
        {
            throw new NotImplementedException();
        }

        public List<Wedding> GetWeddingByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public Wedding GetWeddingById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Wedding> GetWeddingByPlannerId(int plannerId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateWedding(Wedding wedding)
        {
            throw new NotImplementedException();
        }
    }
}
