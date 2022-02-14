using System;
using System.IO;
using System.Net.Mime;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Reports
{
    internal class CarsTextReport: ReportType
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarsTextReport(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override FileStreamResult GenerateReport()
        {
            var cars = _unitOfWork.CarRepository.GetAll();
            MemoryStream ms = new();
            StreamWriter sw = new(ms);
            foreach (var car in cars)
            {
                sw.WriteLine($"{car.Id} {car.Brand} {car.Color}");
            }

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            FileStreamResult result = new(ms, MediaTypeNames.Text.Plain)
            {
                FileDownloadName = $"Cars text report {DateTime.Now}.txt",
            };

            return result;
        }
    }
}