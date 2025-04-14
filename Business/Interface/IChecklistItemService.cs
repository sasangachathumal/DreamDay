using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IChecklistItemService
    {
        List<ChecklistItem> GetChecklistItemsByWeddingId(int weddingId);
        ChecklistItem GetChecklistItemById(int id);
        bool AddChecklistItem(ChecklistItem checklistItem);
        bool UpdateChecklistItem(ChecklistItem checklistItem);
        bool MarkAsDone(int id);
        bool DeleteChecklistItem(int id);
    }
}
