using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Reports
{
    internal class UsersJsonReport : ReportType
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersJsonReport(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override FileStreamResult GenerateReport()
        {
            var users = _unitOfWork.OwnerRepository.GetAll();
            MemoryStream ms = new();
            StreamWriter sw = new(ms);

            sw.WriteLine(JsonSerializer.Serialize(users));

            sw.WriteLine("asasasa");
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            FileStreamResult result = new (ms, MediaTypeNames.Text.Plain)
            {
                FileDownloadName = "test.txt",
            };

            return result;
        }
    }
}
