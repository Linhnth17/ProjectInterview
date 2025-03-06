using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectInterview.Models;
using ProjectInterview.Services;

namespace ProjectInterview.Controllers
{
    public class DataController : Controller
    {
        // Dependencies for CSV service and logging
        private readonly ICsvService _csvService;
        private readonly ILogger<DataController> _logger;

        // Constructor to inject dependencies (ICsvService and ILogger)
        public DataController(ICsvService csvService, ILogger<DataController> logger)
        {
            _csvService = csvService;
            _logger = logger;
        }

        // Index action returns the default view for the DataController
        public IActionResult Index()
        {
            return View("~/Views/Data/Index.cshtml");
        }

        // Handles the file upload and CSV parsing
        //[HttpPost]
        //public ActionResult UploadCsv(IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        // Validate the uploaded file type (only CSV allowed)
        //        if (Path.GetExtension(file.FileName).ToLower() != ".csv")
        //        {
        //            _logger.LogWarning("Invalid file uploaded: {FileName}", file.FileName);
        //            ViewBag.ErrorMessage = "Please upload a valid CSV file.";
        //            return View("Index");
        //        }

        //        try
        //        {
        //            _logger.LogInformation("Processing file: {FileName}", file.FileName);

        //            // Parse the CSV file
        //            List<DataEntry> dataList = _csvService.ParseCsvFile(file.OpenReadStream());
        //            ViewBag.ExcelData = dataList;

        //            // Log success
        //            _logger.LogInformation("{RecordCount} records parsed from {FileName}.", dataList.Count, file.FileName);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log error if something goes wrong
        //            _logger.LogError(ex, "Error processing the CSV file: {FileName}", file.FileName);
        //            ViewBag.ErrorMessage = "An error occurred while processing the CSV file.";
        //        }
        //    }
        //    else
        //    {
        //        // Handle the case where no file is uploaded
        //        _logger.LogWarning("No file uploaded.");
        //        ViewBag.ErrorMessage = "Please upload a valid CSV file.";
        //    }

        //    // Return the Index view with data or error message
        //    return View("Index");
        //}

        [HttpPost]
        public async Task<List<DataEntry>> UploadCsv1(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new List<DataEntry>();
            }
            try
            {
                List<DataEntry> dataList = await _csvService.ParseCsvFileAsync(file.OpenReadStream());
                return dataList;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error processing the CSV file.");
                return new List<DataEntry>();
            }
        }

    }
}
