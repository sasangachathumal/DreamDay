using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;

namespace DreamDay.Business.Service
{
    public class BudgetService : IBudgetService
    {
        private readonly ApplicationDbContext _context;
        public BudgetService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public Task<bool> AddBudget(Budget budget)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBudget(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Budget>> GetAllBudgets()
        {
            throw new NotImplementedException();
        }

        public Task<Budget> GetBudgetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Budget>> GetBudgetsByCategory(BudgetCategory budgetCategory)
        {
            throw new NotImplementedException();
        }

        public Task<List<Budget>> GetBudgetsByWeddingId(int weddingId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBudget(Budget budget)
        {
            throw new NotImplementedException();
        }
    }
}
