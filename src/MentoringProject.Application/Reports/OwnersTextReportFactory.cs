using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Reports
{
    internal class OwnersTextReportFactory : ReportFactory
    {
        private readonly IUnitOfWork _unitOfWork;

        public OwnersTextReportFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override ReportType GetReportType()
        {
            return new OwnersTextReport(_unitOfWork);
        }
    }
}
