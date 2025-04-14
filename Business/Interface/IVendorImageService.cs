using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorImageService
    {
        List<VendorImages> GetAppImagesByVendorID(int VebdorId);
        VendorImages GetAppImageById(int id);
        bool AddAppImage(VendorImages appImage);
        bool DeleteAppImage(int id);
    }
}
