using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;

namespace DreamDay.Business.Service
{
    public class AppImageService : IAppImageService
    {
        private readonly ApplicationDbContext _context;
        public AppImageService(ApplicationDbContext dbContext) 
        {
            _context = dbContext;
        }
        public Task<bool> AddAppImage(AppImages appImage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAppImage(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppImages>> GetAllAppImages()
        {
            throw new NotImplementedException();
        }

        public Task<AppImages> GetAppImageByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppImages>> GetAppImagesByRelatedId(int RelatedId, ImageRelateType relateType)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAppImage(AppImages appImage)
        {
            throw new NotImplementedException();
        }
    }
}
