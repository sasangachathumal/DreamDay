using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamDay.Business.Service
{
    public class ChecklistItemService : IChecklistItemService
    {
        private readonly ApplicationDbContext _context;
        public ChecklistItemService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public bool AddChecklistItem(ChecklistItem checklistItem)
        {
            if (checklistItem == null)
            {
                return false;
            }
            try
            {
                _context.ChecklistItems.Add(checklistItem);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error adding checklist item: {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }

        public bool DeleteChecklistItem(int id)
        {
            if (id == 0)
            {
                return false;
            }
            try
            {
                var checklistItem = _context.ChecklistItems
                .Include(c => c.Wedding)
                .FirstOrDefault(m => m.Id == id);
                if (checklistItem == null)
                {
                    return false;
                }
                else
                {
                    _context.ChecklistItems.Remove(checklistItem);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error deleting checklist item (ID: {id}): {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }

        public List<ChecklistItem> GetAllChecklistItems()
        {
            throw new NotImplementedException();
        }

        public ChecklistItem GetChecklistItemById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            try
            {
                var checklistItem = _context.ChecklistItems
                .Include(c => c.Wedding)
                .FirstOrDefault(m => m.Id == id);
                if (checklistItem == null)
                {
                    return null;
                }
                return checklistItem;
            }
            catch (Exception ex) 
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error marking checklist item as done (ID: {id}): {ex.Message}");
                // Optionally return false or a specific error code/message
                return null;
            }
        }

        public List<ChecklistItem> GetChecklistItemsByWeddingId(int weddingId)
        {
            if (weddingId == 0)
            {
                return new List<ChecklistItem>();
            }

            try
            {
                var weddingChecklistItems = _context.ChecklistItems
                .Where(m => m.WeddingId == weddingId)
                .ToList();

                return weddingChecklistItems;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error retrieving checklist items for wedding (ID: {weddingId}): {ex.Message}");
                // Optionally return an empty list or a specific error code/message
                return new List<ChecklistItem>();
            }
        }

        public List<ChecklistItem> GetChecklistItemsOfWeddingByDate(int weddingId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<ChecklistItem> GetChecklistItemsOfWeddingByStatus(int weddingId, bool isDone)
        {
            throw new NotImplementedException();
        }

        public bool MarkAsDone(int id)
        {
            if (id == 0)
            {
                return false;
            }

            try
            {
                var checklistItem = _context.ChecklistItems.Find(id);
                if (checklistItem == null)
                {
                    return false;
                }
                checklistItem.IsDone = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error marking checklist item as done (ID: {id}): {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }

        public bool UpdateChecklistItem(ChecklistItem checklistItem)
        {
            if (checklistItem == null)
            {
                return false;
            }
            try
            {
                _context.Update(checklistItem);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error updating checklist item: {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }
    }
}
