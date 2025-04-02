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
        public bool AddBudget(Budget budget)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBudget(int id)
        {
            throw new NotImplementedException();
        }

        public List<Budget> GetAllBudgets()
        {
            throw new NotImplementedException();
        }

        public Budget GetBudgetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Budget> GetBudgetsByCategory(BudgetCategory budgetCategory)
        {
            throw new NotImplementedException();
        }

        public List<Budget> GetBudgetsByWeddingId(int weddingId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBudget(Budget budget)
        {
            throw new NotImplementedException();
        }
    }
}
