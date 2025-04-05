using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IBudgetService
    {
        List<Budget> GetAllBudgets();
        List<Budget> GetBudgetsByWeddingId(int weddingId);
        List<Budget> GetBudgetsByCategory(BudgetCategory budgetCategory);
        Budget GetBudgetById(int id);
        bool AddBudget(Budget budget);
        bool UpdateBudget(Budget budget);
        bool DeleteBudget(int id);
    }
}
