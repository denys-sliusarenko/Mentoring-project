using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Reports
{
    internal abstract class ReportType
    {
        public abstract FileStreamResult GenerateReport();
    }
}
