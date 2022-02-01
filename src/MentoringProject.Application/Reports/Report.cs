using MentoringProject.Domain.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

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
