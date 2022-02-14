using MentoringProject.Domain.Core.Interfaces.Repositories;

namespace MentoringProject.Application.Reports
{
    internal class CarsTextReportFactory : ReportFactory
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarsTextReportFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override ReportType GetReportType()
        {
            return new CarsTextReport(_unitOfWork);
        }
    }
}
