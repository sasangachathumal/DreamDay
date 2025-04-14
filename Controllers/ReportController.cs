using DreamDay.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;
using System.Linq;

namespace DreamDay.Controllers
{
    public class ReportController : Controller
    {
        private readonly IVendorService _vendorService;
        private readonly IWeddingService _weddingService;

        public ReportController(IVendorService vendorService, IWeddingService weddingService)
        {
            _vendorService = vendorService;
            _weddingService = weddingService;
        }

        // View with both Vendor and Wedding charts
        public IActionResult ReportAndAnalysis()
        {
            var vendors = _vendorService.GetAllVendors();
            var weddings = _weddingService.GetAllWeddings();

            // Vendor category chart data
            var vendorChartData = vendors
                .GroupBy(v => v.VendorCategory?.Name ?? "Uncategorized")
                .Select(g => new
                {
                    Category = g.Key,
                    Count = g.Count()
                }).ToList();

            // Wedding by month chart data
            var weddingChartData = weddings
                .GroupBy(w => w.WeddingDate.ToString("yyyy-MM"))
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                }).OrderBy(g => g.Month).ToList();

            ViewBag.VendorChartData = vendorChartData;
            ViewBag.WeddingChartData = weddingChartData;

            return View("~/Views/Report/ReportAndAnalysis.cshtml");
        }

        // Excel for vendors
        public IActionResult ExportVendorExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var vendors = _vendorService.GetAllVendors();

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Vendors");

            ws.Cells["A1"].Value = "ID";
            ws.Cells["B1"].Value = "Name";
            ws.Cells["C1"].Value = "Category";
            ws.Cells["D1"].Value = "Phone";
            ws.Cells["E1"].Value = "Email";
            ws.Cells["F1"].Value = "Address";

            int row = 2;
            foreach (var v in vendors)
            {
                ws.Cells[row, 1].Value = v.Id;
                ws.Cells[row, 2].Value = v.Name;
                ws.Cells[row, 3].Value = v.VendorCategory?.Name ?? "Uncategorized";
                ws.Cells[row, 4].Value = v.Phone;
                ws.Cells[row, 5].Value = v.Email;
                ws.Cells[row, 6].Value = v.Address;
                row++;
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VendorReport.xlsx");
        }

        // Excel for weddings
        public IActionResult ExportWeddingExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var weddings = _weddingService.GetAllWeddings();

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Weddings");

            ws.Cells["A1"].Value = "ID";
            ws.Cells["B1"].Value = "Title";
            ws.Cells["C1"].Value = "Date";
            ws.Cells["D1"].Value = "Client";
            ws.Cells["E1"].Value = "Planner";

            int row = 2;
            foreach (var w in weddings)
            {
                var clientName = w.Client?.FullName ?? "Unknown Client";
                var plannerName = w.Planner?.FullName ?? "Unassigned";
                var title = $"Wedding of {clientName}";

                ws.Cells[row, 1].Value = w.Id;
                ws.Cells[row, 2].Value = title;
                ws.Cells[row, 3].Value = w.WeddingDate.ToString("yyyy-MM-dd");
                ws.Cells[row, 4].Value = clientName;
                ws.Cells[row, 5].Value = plannerName;
                row++;
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "WeddingReport.xlsx");
        }
    }
}
