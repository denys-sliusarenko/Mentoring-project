using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Interfaces
{
    public interface IReportService
    {
        FileStreamResult GenerateOwnersTextReport();

        FileStreamResult GenerateOwnerCarsTextReport();
    }
}
