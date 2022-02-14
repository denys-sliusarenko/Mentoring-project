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
        [Route("ownersTextReport")]
        public IActionResult GenerateOwnerssTextReport()
        {
            var file = _reportService.GenerateOwnersTextReport();
            return file;
        }

        [HttpGet]
        [Route("ownerCarsTextReport")]
        public IActionResult GenerateOwnerCarsTextReport()
        {
            var file = _reportService.GenerateOwnerCarsTextReport();
            MemoryStream ms = new();
            file.FileStream.CopyTo(ms);
            return Ok(new
            {
                blob = ms.ToArray(),
                fileName = file.FileDownloadName,
                fileType = file.ContentType,
            });

           // return file;
        }
    }
}