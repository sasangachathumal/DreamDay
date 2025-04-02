using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IChecklistItemService
    {
        List<ChecklistItem> GetAllChecklistItems();
        List<ChecklistItem> GetChecklistItemsByWeddingId(int weddingId);
        List<ChecklistItem> GetChecklistItemsOfWeddingByStatus(int weddingId, bool isDone);
        List<ChecklistItem> GetChecklistItemsOfWeddingByDate(int weddingId, DateTime date);
        ChecklistItem GetChecklistItemByIdAsync(int id);
        bool AddChecklistItem(ChecklistItem checklistItem);
        bool UpdateChecklistItem(ChecklistItem checklistItem);
        bool DeleteChecklistItem(int id);
    }
}
