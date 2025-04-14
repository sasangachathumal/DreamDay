using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface ITimelineService
    {
        List<Timeline> GetTimelinesByWeddingId(int weddingId);
        Timeline GetTimelineById(int id);
        bool AddTimeline(Timeline timeline);
        bool UpdateTimeline(Timeline timeline);
        bool DeleteTimeline(int id);
        bool MarkAsDone(int id);
    }
}
