using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamDay.Data;
using DreamDay.Models;
using DreamDay.Business.Interface;
using DreamDay.Business.Service;

namespace DreamDay.Controllers
{
    public class VendorImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVendorImageService _vendorImageService;

        public VendorImagesController(ApplicationDbContext context, IVendorImageService vendorImageService)
        {
            _context = context;
            _vendorImageService = vendorImageService;
        }

        // GET: VendorImages
        public async Task<IActionResult> Index(int vendorId)
        {
            int _vendorId = 0;
            if (vendorId > 0)
            {
                HttpContext.Session.SetInt32("VendorId", vendorId);
                _vendorId = vendorId;
            }
            else
            {
                var sessionVendorId = HttpContext.Session.GetInt32("VendorId");
                if (sessionVendorId.HasValue)
                {
                    _vendorId = sessionVendorId.Value;
                }
            }
            var images = _vendorImageService.GetAppImagesByVendorID(_vendorId);
            return View(images);
        }

        // GET: VendorImages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendorImageUploadViewModel vendorImageUpload)
        {
            if (ModelState.IsValid && vendorImageUpload.ImageFile != null)
            {

                // 1. Generate unique filename
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vendorImageUpload.ImageFile.FileName);

                // 2. Define path
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                // 3. Create folder if not exists
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // 4. Full file path
                string filePath = Path.Combine(uploadPath, fileName);

                // 5. Save file to wwwroot/uploads
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vendorImageUpload.ImageFile.CopyToAsync(stream);
                }

                // 6. Save relative path to DB
                int? vendorId = HttpContext.Session.GetInt32("VendorId");
                if (vendorId.HasValue)
                {
                    var image = new VendorImages
                    {
                        ImageURL = "/uploads/" + fileName,
                        VendorId = vendorId.Value
                    };
                    _vendorImageService.AddAppImage(image);
                    return RedirectToAction(nameof(Index), new { VendorId = vendorId });
                }
                else
                {
                    ModelState.AddModelError("", "Vendor Id is not set in session.");
                }
            }
            return View(vendorImageUpload);
        }

        // GET: VendorImages/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            _vendorImageService.DeleteAppImage(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
