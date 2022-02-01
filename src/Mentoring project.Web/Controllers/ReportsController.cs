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
        [Route("usersTextReport")]
        public IActionResult GenerateUsersTextReport()
        {
            var file = _reportService.GenerateUsersTextReport();
            return file;
        }
    }
}