using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IChecklistItemService
    {
        Task<List<ChecklistItem>> GetAllChecklistItems();
        Task<List<ChecklistItem>> GetChecklistItemsByWeddingId(int weddingId);
        Task<List<ChecklistItem>> GetChecklistItemsOfWeddingByStatus(int weddingId, bool isDone);
        Task<List<ChecklistItem>> GetChecklistItemsOfWeddingByDate(int weddingId, DateTime date);
        Task<ChecklistItem> GetChecklistItemByIdAsync(int id);
        Task<bool> AddChecklistItem(ChecklistItem checklistItem);
        Task<bool> UpdateChecklistItem(ChecklistItem checklistItem);
        Task<bool> DeleteChecklistItem(int id);
    }
}
