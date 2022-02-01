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
    internal abstract class ReportType
    {

        public abstract FileStreamResult GenerateReport();
    }
}
