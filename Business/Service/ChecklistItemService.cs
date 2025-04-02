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
            throw new NotImplementedException();
        }

        public bool DeleteChecklistItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<ChecklistItem> GetAllChecklistItems()
        {
            throw new NotImplementedException();
        }

        public ChecklistItem GetChecklistItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<ChecklistItem> GetChecklistItemsByWeddingId(int weddingId)
        {
            if (weddingId == 0)
            {
                return new List<ChecklistItem>();
            }

            var weddingChecklistItems = _context.ChecklistItems
                .Where(m => m.WeddingId == weddingId)
                .ToList();

            return weddingChecklistItems;
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
            throw new NotImplementedException();
        }
    }
}
