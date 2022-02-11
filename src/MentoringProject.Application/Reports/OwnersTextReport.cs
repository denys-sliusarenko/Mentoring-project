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
    internal class OwnersTextReport : ReportType
    {
        private readonly IUnitOfWork _unitOfWork;

        public OwnersTextReport(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override FileStreamResult GenerateReport()
        {
            var owners = _unitOfWork.OwnerRepository.GetAll();
            MemoryStream ms = new ();
            StreamWriter sw = new (ms);
            foreach (var t in owners)
            {
                sw.WriteLine($"{t.Id} {t.FirstName} {t.LastName}");
            }

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            FileStreamResult result = new (ms, MediaTypeNames.Text.Plain)
            {
                FileDownloadName = $"Owners text report {DateTime.Now}.txt",
            };

            return result;
        }
    }
}
