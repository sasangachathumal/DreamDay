using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IBudgetService
    {
        Task<List<Budget>> GetAllBudgets();
        Task<List<Budget>> GetBudgetsByWeddingId(int weddingId);
        Task<List<Budget>> GetBudgetsByCategory(BudgetCategory budgetCategory);
        Task<Budget> GetBudgetByIdAsync(int id);
        Task<bool> AddBudget(Budget budget);
        Task<bool> UpdateBudget(Budget budget);
        Task<bool> DeleteBudget(int id);
    }
}
