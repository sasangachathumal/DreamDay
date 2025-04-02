using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IWeddingService
    {
        List<Wedding> GetAllWeddings();
        List<Wedding> GetWeddingByClientId(int clientId);
        List<Wedding> GetWeddingByPlannerId(int plannerId);
        Wedding GetWeddingById(int id);
        bool AddWedding(Wedding wedding);
        bool UpdateWedding(Wedding wedding);
        bool DeleteWedding(int id);
    }
}
