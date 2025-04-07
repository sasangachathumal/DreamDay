using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IWeddingService
    {
        List<Wedding> GetAllWeddings();
        Wedding GetWeddingByClientId(string clientId);
        List<Wedding> GetWeddingByPlannerId(string plannerId);
        Wedding GetWeddingById(int id);
        bool AddWedding(Wedding wedding);
        bool UpdateWedding(Wedding wedding);
        bool DeleteWedding(int id);
    }
}
