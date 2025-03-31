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
        public Task<bool> AddTimeline(Timeline timeline)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTimeline(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Timeline>> GetAllTimelines()
        {
            throw new NotImplementedException();
        }

        public Task<Timeline> GetTimelineByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Timeline>> GetTimelinesByWeddingId(int weddingId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Timeline>> GetTimelinesOfWeddingByStartTime(int weddingId, DateTime startTime)
        {
            throw new NotImplementedException();
        }

        public Task<List<Timeline>> GetTimelinesOfWeddingByStatus(int weddingId, bool isDone)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTimeline(Timeline timeline)
        {
            throw new NotImplementedException();
        }
    }
}
