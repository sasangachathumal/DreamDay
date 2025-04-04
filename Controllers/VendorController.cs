using DreamDay.Business.Interface;
using DreamDay.Models;
using Microsoft.AspNetCore.Mvc;

namespace DreamDay.Controllers
{
    public class VendorController : Controller
    {
        private readonly IVendorCategoryService _vendorCategoryService;
        private readonly IVendorService _vendorService;
        private readonly IVendorPackageService _vendorPackageService;
        public VendorController(IVendorCategoryService vendorCategoryService, IVendorService vendorService, IVendorPackageService vendorPackageService)
        {
            _vendorCategoryService = vendorCategoryService;
            _vendorService = vendorService;
            _vendorPackageService = vendorPackageService;
        }

        //  Display the form to add a new vendor category
        public IActionResult VendorCategory()
        {
            return View("~/Views/Vendor/VendorCategory.cshtml"); 
        }

        //  Handle the form submission for adding a new vendor category
        [HttpPost]
        public IActionResult VendorCategory(VendorCategory model)
        {
            if (ModelState.IsValid)
            {
                bool success = _vendorCategoryService.AddVendorCategory(model);
                if (success)
                {
                    TempData["SuccessMessage"] = "Vendor Category added successfully!";
                    return RedirectToAction("VendorCategoryList"); // ✅ Redirects to VendorCategoryList
                }
                else
                {
                    ViewData["ErrorMessage"] = "Failed to add Vendor Category.";
                }
            }
            return View(model);
        }


        //  Display the list of vendor categories
        public IActionResult VendorCategoryList()
        {
            var categories = _vendorCategoryService.GetAllVendorCategories();
            return View("~/Views/Vendor/VendorCategoryList.cshtml", categories);
        }

        //  Delete vendor category
        [HttpPost]
        public IActionResult DeleteVendorCategory(int id)
        {
            _vendorCategoryService.DeleteVendorCategory(id);
            return RedirectToAction("VendorCategoryList");
        }

        //  Edit vendor category
        public IActionResult EditVendorCategory(int id)
        {
            var category = _vendorCategoryService.GetVendorCategoryById(id);
            return View("~/Views/Vendor/EditVendorCategory.cshtml", category);
        }

        //  Handle the form submission for editing a vendor category
        [HttpPost]
        public IActionResult EditVendorCategory(VendorCategory model)
        {
            if (ModelState.IsValid)
            {
                _vendorCategoryService.UpdateVendorCategory(model);
                return RedirectToAction("VendorCategoryList");
            }
            return View(model);
        }






        // Display the Add Vendor form
        public IActionResult Vendor()
        {
            var vendorCategories = _vendorCategoryService.GetAllVendorCategories();
            ViewBag.VendorCategories = vendorCategories;
            return View("~/Views/Vendor/Vendor.cshtml");
        }

        [HttpPost]
        public IActionResult Vendor(Vendor model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool success = _vendorService.AddVendor(model);
                    if (success)
                    {
                        TempData["SuccessMessage"] = "Vendor added successfully!";
                        return RedirectToAction("VendorCategoryList");
                    }
                    else
                    {
                        throw new Exception("Failed to add Vendor. Possible database constraint error.");
                    }
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Validation error! Check the input fields.";
            }

            // Reload Vendor Categories in case of an error
            ViewBag.VendorCategories = _vendorCategoryService.GetAllVendorCategories();
            return View("~/Views/Vendor/Vendor.cshtml", model);
        }


        //Add pacage

        public IActionResult AddPackage()
        {
            ViewBag.Vendors = _vendorService.GetAllVendors();
            return View("~/Views/Vendor/AddPackage.cshtml");
        }

        [HttpPost]
        public IActionResult AddPackage(VendorPackage model)
        {
            if (ModelState.IsValid)
            {
                _vendorPackageService.AddVendorPackage(model);
                TempData["SuccessMessage"] = "Package added successfully!";
                return RedirectToAction("PackageList");
            }

            ViewBag.Vendors = _vendorService.GetAllVendors();
            return View("~/Views/Vendor/AddPackage.cshtml", model);
        }

    }
}
