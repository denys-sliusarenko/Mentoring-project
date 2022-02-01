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
    internal class UsersTextReport : ReportType
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersTextReport(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override FileStreamResult GenerateReport()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            MemoryStream ms = new ();
            StreamWriter sw = new (ms);
            foreach (var t in users)
            {
                sw.WriteLine($"{t.UserId} {t.FirstName} {t.LastName}");
            }

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            FileStreamResult result = new (ms, MediaTypeNames.Text.Plain)
            {
                FileDownloadName = $"Users text report {DateTime.Now}.txt",
            };

            return result;
        }
    }
}
