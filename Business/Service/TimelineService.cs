using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;

namespace DreamDay.Business.Service
{
    public class TimelineService : ITimelineService
    {
        private readonly ApplicationDbContext _context;
        public TimelineService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public bool AddTimeline(Timeline timeline)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTimeline(int id)
        {
            throw new NotImplementedException();
        }

        public List<Timeline> GetAllTimelines()
        {
            throw new NotImplementedException();
        }

        public Timeline GetTimelineByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Timeline> GetTimelinesByWeddingId(int weddingId)
        {
            throw new NotImplementedException();
        }

        public List<Timeline> GetTimelinesOfWeddingByStartTime(int weddingId, DateTime startTime)
        {
            throw new NotImplementedException();
        }

        public List<Timeline> GetTimelinesOfWeddingByStatus(int weddingId, bool isDone)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTimeline(Timeline timeline)
        {
            throw new NotImplementedException();
        }
    }
}
