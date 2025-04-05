using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.EntityFrameworkCore;

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
            if (timeline == null)
            {
                return false;
            }
            try
            {
                _context.Timelines.Add(timeline);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error adding timeline item: {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }

        public bool DeleteTimeline(int id)
        {
            if (id == 0)
            {
                return false;
            }
            try
            {
                var timeline = _context.Timelines
                .Include(c => c.Wedding)
                .FirstOrDefault(m => m.Id == id);
                if (timeline == null)
                {
                    return false;
                }
                else
                {
                    _context.Timelines.Remove(timeline);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error deleting timeline item (ID: {id}): {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }

        public List<Timeline> GetAllTimelines()
        {
            throw new NotImplementedException();
        }

        public Timeline GetTimelineById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            try
            {
                var timeline = _context.Timelines
                .Include(c => c.Wedding)
                .FirstOrDefault(m => m.Id == id);
                if (timeline == null)
                {
                    return null;
                }
                return timeline;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error on getting timeline item: {ex.Message}");
                // Optionally return false or a specific error code/message
                return null;
            }
        }

        public List<Timeline> GetTimelinesByWeddingId(int weddingId)
        {
            if (weddingId == 0)
            {
                return new List<Timeline>();
            }

            try
            {
                var weddingTimelines = _context.Timelines
                .Where(m => m.WeddingId == weddingId)
                .ToList();

                return weddingTimelines;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error retrieving timeline items for wedding (ID: {weddingId}): {ex.Message}");
                // Optionally return an empty list or a specific error code/message
                return new List<Timeline>();
            }
        }

        public List<Timeline> GetTimelinesOfWeddingByStartTime(int weddingId, DateTime startTime)
        {
            throw new NotImplementedException();
        }

        public List<Timeline> GetTimelinesOfWeddingByStatus(int weddingId, bool isDone)
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
                var timeline = _context.Timelines.Find(id);
                if (timeline == null)
                {
                    return false;
                }
                timeline.IsDone = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error marking timeline item as done (ID: {id}): {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }

        public bool UpdateTimeline(Timeline timeline)
        {
            if (timeline == null)
            {
                return false;
            }
            try
            {
                _context.Update(timeline);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error updating timeline item: {ex.Message}");
                // Optionally return false or a specific error code/message
                return false;
            }
        }
    }
}
