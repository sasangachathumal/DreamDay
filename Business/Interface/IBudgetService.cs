using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IBudgetService
    {
        List<Budget> GetBudgetsByWeddingId(int weddingId);
        List<Budget> GetAllBudgets();
        Budget GetBudgetById(int id);
        bool AddBudget(Budget budget);
        bool UpdateBudget(Budget budget);
        bool DeleteBudget(int id);
    }
}
