using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface ITimelineService
    {
        List<Timeline> GetAllTimelines();
        List<Timeline> GetTimelinesByWeddingId(int weddingId);
        List<Timeline> GetTimelinesOfWeddingByStartTime(int weddingId, DateTime startTime);
        List<Timeline> GetTimelinesOfWeddingByStatus(int weddingId, bool isDone);
        Timeline GetTimelineByIdAsync(int id);
        bool AddTimeline(Timeline timeline);
        bool UpdateTimeline(Timeline timeline);
        bool DeleteTimeline(int id);
    }
}
