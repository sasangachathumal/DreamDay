using DreamDay.Business.Interface;
using DreamDay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult VendorAdd()
        {
            var vendorCategories = _vendorCategoryService.GetAllVendorCategories();
            ViewBag.VendorCategories = vendorCategories;
            return View("~/Views/Vendor/VendorAdd.cshtml");
        }
        
        // Vendor Add
        [HttpPost]
        public IActionResult VendorAdd(Vendor model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool success = _vendorService.AddVendor(model);
                    if (success)
                    {
                        TempData["SuccessMessage"] = "Vendor added successfully!";
                        return RedirectToAction("VendorList");
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
            return View("~/Views/Vendor/VendorAdd.cshtml", model);
        }


        public IActionResult VendorList()
        {
            var vendors = _vendorService.GetAllVendors(); // Includes VendorCategory via Include
            return View("~/Views/Vendor/VendorList.cshtml", vendors);
        }


        // Delete Vendor
        public IActionResult DeleteVendor(int id)
        {
            var success = _vendorService.DeleteVendor(id);
            if (success)
            {
                TempData["SuccessMessage"] = "Vendor deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete vendor!";
            }

            return RedirectToAction("VendorList");
        }

        // GET: Edit
        public IActionResult EditVendor(int id)
        {
            var vendor = _vendorService.GetVendorById(id); // or await if async
            if (vendor == null) return NotFound();

            ViewBag.VendorCategories = new SelectList(_vendorCategoryService.GetAllVendorCategories(), "Id", "Name", vendor.VendorCategoryId);
            return View("VendorEdit", vendor);
        }

        // POST: Edit
        [HttpPost]
        public IActionResult EditVendor(Vendor model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.VendorCategories = new SelectList(_vendorCategoryService.GetAllVendorCategories(), "Id", "Name", model.VendorCategoryId);
                return View("VendorEdit", model);
            }

            var result = _vendorService.UpdateVendor(model);
            if (result)
            {
                TempData["SuccessMessage"] = "Vendor updated successfully!";
                return RedirectToAction("VendorList");
            }

            TempData["ErrorMessage"] = "Update failed!";
            ViewBag.VendorCategories = new SelectList(_vendorCategoryService.GetAllVendorCategories(), "Id", "Name", model.VendorCategoryId);
            return View("VendorEdit", model);
        }

        //Add Package

        public IActionResult AddPackage()
        {
            ViewBag.Vendors = _vendorService.GetAllVendors();
            return View("AddPackage");
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
            return View("AddPackage", model);
        }

        public IActionResult PackageList()
        {
            var packages = _vendorPackageService.GetAllVendorPackages();
            return View("PackageList", packages);
        }
        public IActionResult DeletePackage(int id)
        {
            bool result = _vendorPackageService.DeleteVendorPackage(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Package deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Package deletion failed!";
            }

            return RedirectToAction("PackageList");
        }

        // GET: EditPackage
        public IActionResult EditPackageList(int id)
        {
            var package = _vendorPackageService.GetVendorPackageById(id);
            if (package == null) return NotFound();

            ViewBag.Vendors = _vendorService.GetAllVendors();
            return View("EditPackageList", package);
        }

        // POST: EditPackage
        [HttpPost]
        public IActionResult EditPackageList(VendorPackage model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Vendors = _vendorService.GetAllVendors();
                return View("EditPackageList", model);
            }

            bool result = _vendorPackageService.UpdateVendorPackage(model);
            if (result)
            {
                TempData["SuccessMessage"] = "Package updated successfully!";
                return RedirectToAction("PackageList");
            }

            TempData["ErrorMessage"] = "Package update failed!";
            ViewBag.Vendors = _vendorService.GetAllVendors();
            return View("EditPackageList", model);
        }



    }
}
