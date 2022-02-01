using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Services
{
    internal abstract class ReportFactory
    {
        public abstract ReportType GetReportType();
    }
}
