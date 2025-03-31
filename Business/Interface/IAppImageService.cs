using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IAppImageService
    {
        Task<List<AppImages>> GetAllAppImages();
        Task<List<AppImages>> GetAppImagesByRelatedId(int RelatedId, ImageRelateType relateType);
        Task<AppImages> GetAppImageByIdAsync(int id);
        Task<bool> AddAppImage(AppImages appImage);
        Task<bool> UpdateAppImage(AppImages appImage);
        Task<bool> DeleteAppImage(int id);
    }
}
