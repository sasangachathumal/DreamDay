using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorReviewService
    {
        List<VendorReviews> GetAllVendorReviews();
        List<VendorReviews> GetVendorReviewsByVendorId(int vendorId);
        List<VendorReviews> GetVendorReviewsByUserId(int userId);
        VendorReviews GetVendorReviewById(int id);
        bool AddVendorReview(VendorReviews vendorReview);
        bool UpdateVendorReview(VendorReviews vendorReview);
        bool DeleteVendorReview(int id);
    }
}
