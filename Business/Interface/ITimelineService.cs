using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface ITimelineService
    {
        Task<List<Timeline>> GetAllTimelines();
        Task<List<Timeline>> GetTimelinesByWeddingId(int weddingId);
        Task<List<Timeline>> GetTimelinesOfWeddingByStartTime(int weddingId, DateTime startTime);
        Task<List<Timeline>> GetTimelinesOfWeddingByStatus(int weddingId, bool isDone);
        Task<Timeline> GetTimelineByIdAsync(int id);
        Task<bool> AddTimeline(Timeline timeline);
        Task<bool> UpdateTimeline(Timeline timeline);
        Task<bool> DeleteTimeline(int id);
    }
}
