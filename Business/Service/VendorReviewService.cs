﻿using DreamDay.Business.Interface;
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
        public bool AddVendorReview(VendorReviews vendorReview)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVendorReview(int id)
        {
            throw new NotImplementedException();
        }

        public List<VendorReviews> GetAllVendorReviews()
        {
            throw new NotImplementedException();
        }

        public VendorReviews GetVendorReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public List<VendorReviews> GetVendorReviewsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public List<VendorReviews> GetVendorReviewsByVendorId(int vendorId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVendorReview(VendorReviews vendorReview)
        {
            throw new NotImplementedException();
        }
    }
}
