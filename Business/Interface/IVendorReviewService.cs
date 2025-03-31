using DreamDay.Models;

namespace DreamDay.Business.Interface
{
    public interface IVendorReviewService
    {
        Task<List<VendorReviews>> GetAllVendorReviews();
        Task<List<VendorReviews>> GetVendorReviewsByVendorId(int vendorId);
        Task<List<VendorReviews>> GetVendorReviewsByUserId(int userId);
        Task<VendorReviews> GetVendorReviewById(int id);
        Task<bool> AddVendorReview(VendorReviews vendorReview);
        Task<bool> UpdateVendorReview(VendorReviews vendorReview);
        Task<bool> DeleteVendorReview(int id);
    }
}
