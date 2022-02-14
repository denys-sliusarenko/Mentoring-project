using MentoringProject.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace MentoringProject.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Route("carsTextReport")]
        public IActionResult GenerateCarsTextReport()
        {
            var file = _reportService.GenerateCarsTextReport();
            return SendReport(file);
            //   return file;
        }

        [HttpGet]
        [Route("ownersTextReport")]
        public IActionResult GenerateOwnersTextReport()
        {
            var file = _reportService.GenerateOwnersTextReport();
            return SendReport(file);
            //   return file;
        }

        [HttpGet]
        [Route("ownerCarsTextReport")]
        public IActionResult GenerateOwnerCarsTextReport()
        {
            var file = _reportService.GenerateOwnerCarsTextReport();
            return SendReport(file);

            // return file;
        }

        private IActionResult SendReport(FileStreamResult file)
        {
            MemoryStream ms = new();
            file.FileStream.CopyTo(ms);
            return Ok(new
            {
                blob = ms.ToArray(),
                fileName = file.FileDownloadName,
                fileType = file.ContentType,
            });
        }
    }
}