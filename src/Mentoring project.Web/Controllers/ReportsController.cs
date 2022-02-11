using MentoringProject.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return file;
        }
    }
}