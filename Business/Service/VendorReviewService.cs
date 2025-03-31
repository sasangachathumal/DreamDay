using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;

namespace DreamDay.Business.Service
{
    public class VendorReviewService : IVendorReviewService
    {
        private readonly ApplicationDbContext _context;
        public VendorReviewService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public Task<bool> AddVendorReview(VendorReviews vendorReview)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteVendorReview(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorReviews>> GetAllVendorReviews()
        {
            throw new NotImplementedException();
        }

        public Task<VendorReviews> GetVendorReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorReviews>> GetVendorReviewsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorReviews>> GetVendorReviewsByVendorId(int vendorId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateVendorReview(VendorReviews vendorReview)
        {
            throw new NotImplementedException();
        }
    }
}
