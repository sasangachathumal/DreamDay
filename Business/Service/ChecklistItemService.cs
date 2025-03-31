using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;

namespace DreamDay.Business.Service
{
    public class ChecklistItemService : IChecklistItemService
    {
        private readonly ApplicationDbContext _context;
        public ChecklistItemService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public Task<bool> AddChecklistItem(ChecklistItem checklistItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteChecklistItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChecklistItem>> GetAllChecklistItems()
        {
            throw new NotImplementedException();
        }

        public Task<ChecklistItem> GetChecklistItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChecklistItem>> GetChecklistItemsByWeddingId(int weddingId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChecklistItem>> GetChecklistItemsOfWeddingByDate(int weddingId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChecklistItem>> GetChecklistItemsOfWeddingByStatus(int weddingId, bool isDone)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateChecklistItem(ChecklistItem checklistItem)
        {
            throw new NotImplementedException();
        }
    }
}
