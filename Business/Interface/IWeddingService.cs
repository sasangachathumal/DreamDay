using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IWeddingService
    {
        Task<List<Wedding>> GetAllWeddings();
        Task<List<Wedding>> GetWeddingByClientId(int clientId);
        Task<List<Wedding>> GetWeddingByPlannerId(int plannerId);
        Task<Wedding> GetWeddingById(int id);
        Task<bool> AddWedding(Wedding wedding);
        Task<bool> UpdateWedding(Wedding wedding);
        Task<bool> DeleteWedding(int id);
    }
}
