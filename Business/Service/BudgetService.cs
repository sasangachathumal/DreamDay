using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.EntityFrameworkCore;

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
            if (budget == null)
            {
                return false;
            }
            try
            {
                _context.Budgets.Add(budget);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error adding budget item: {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }

        public bool DeleteBudget(int id)
        {
            if (id == 0)
            {
                return false;
            }
            try
            {
                var budgetItem = _context.Budgets
                .Include(c => c.Wedding)
                .FirstOrDefault(m => m.Id == id);
                if (budgetItem == null)
                {
                    return false;
                }
                else
                {
                    _context.Budgets.Remove(budgetItem);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error deleting budget item (ID: {id}): {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }

        public List<Budget> GetAllBudgets()
        {
            throw new NotImplementedException();
        }

        public Budget GetBudgetById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            try
            {
                var budgetItem = _context.Budgets
                .Include(c => c.Wedding)
                .FirstOrDefault(m => m.Id == id);
                if (budgetItem == null)
                {
                    return null;
                }
                return budgetItem;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error on getting budget item: {ex.Message}");
                // Optionally return false or a specific error code/message
                return null;
            }
        }

        public List<Budget> GetBudgetsByCategory(BudgetCategory budgetCategory)
        {
            throw new NotImplementedException();
        }

        public List<Budget> GetBudgetsByWeddingId(int weddingId)
        {
            if (weddingId == 0)
            {
                return new List<Budget>();
            }

            try
            {
                var weddingBudgetsItems = _context.Budgets
                .Where(m => m.WeddingId == weddingId)
                .ToList();

                return weddingBudgetsItems;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error retrieving budgets items for wedding (ID: {weddingId}): {ex.Message}");
                // Optionally return an empty list or a specific error code/message
                return new List<Budget>();
            }
        }

        public bool UpdateBudget(Budget budget)
        {
            if (budget == null)
            {
                return false;
            }
            try
            {
                _context.Update(budget);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error updating budget item: {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }
    }
}
