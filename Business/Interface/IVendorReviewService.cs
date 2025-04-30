using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorReviewService
    {
        List<VendorReviews> GetVendorReviewsByVendorId(int vendorId);
        List<VendorReviews> GetVendorReviewsById(int id);
        List<VendorReviews> GeAlltVendorReviews();
        bool AddVendorReview(VendorReviews vendorReview);
        bool DeleteVendorReview(int id);
        VendorReviews GetVendorReviewById(int id); // change name and return type

    }
}
