using MentoringProject.Domain.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Reports
{
    internal class Report
    {
        private ReportType _reportType;

        public Report(ReportFactory factory)
        {
            _reportType = factory.GetReportType();
        }

        public FileStreamResult GenerateReport()
        {
            var report = _reportType.GenerateReport();
            return report;
        }
    }
}
