using MentoringProject.Domain.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Application.Reports
{
    internal class OwnerCarsTextReportFactory : ReportFactory
    {
        private readonly IUnitOfWork _unitOfWork;

        public OwnerCarsTextReportFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override ReportType GetReportType()
        {
            return new OwnersCarsTextReport(_unitOfWork);
        }
    }
}
