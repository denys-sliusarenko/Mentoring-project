using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentoringProject.Application.Interfaces;
using MentoringProject.Application.Reports;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FileStreamResult GenerateUsersTextReport()
        {
            Report report = new Report(new UserTextReportFactory(_unitOfWork));
            return report.GenerateReport();
        }
    }
}
