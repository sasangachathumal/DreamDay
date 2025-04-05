using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IAppImageService
    {
        List<AppImages> GetAllAppImages();
        List<AppImages> GetAppImagesByRelatedId(int RelatedId, ImageRelateType relateType);
        AppImages GetAppImageByIdAsync(int id);
        bool AddAppImage(AppImages appImage);
        bool UpdateAppImage(AppImages appImage);
        bool DeleteAppImage(int id);
    }
}
