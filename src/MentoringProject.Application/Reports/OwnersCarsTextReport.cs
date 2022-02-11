using System;
using System.IO;
using System.Net.Mime;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Reports
{
    internal class OwnersCarsTextReport : ReportType
    {
        private readonly IUnitOfWork _unitOfWork;

        public OwnersCarsTextReport(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override FileStreamResult GenerateReport()
        {
            var owners = _unitOfWork.OwnerRepository.GetAll();
            MemoryStream ms = new();
            StreamWriter sw = new(ms);
            foreach (var owner in owners)
            {
                sw.WriteLine($"{owner.Id} {owner.FirstName} {owner.LastName}");
                sw.WriteLine();
                foreach (var ownercars in owner.OwnersCars)
                {
                    sw.WriteLine($"Car id: {ownercars.CarId}");
                    sw.WriteLine($"Car brand: {ownercars.Car.Brand}");
                    sw.WriteLine($"Car color: {ownercars.Car.Color}");
                    sw.WriteLine($"Registration number: {ownercars.RegistrationNumber}");
                    sw.WriteLine();
                }

                sw.WriteLine();
                sw.WriteLine(new string('=', 10));
                sw.WriteLine();
            }

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            FileStreamResult result = new(ms, MediaTypeNames.Text.Plain)
            {
                FileDownloadName = $"Owner cars text report {DateTime.Now}.txt",
            };

            return result;
        }
    }
}