using MentoringProject.Application.Interfaces;
using MentoringProject.Application.Services;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using MentoringProject.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace MentoringProject.Infrastructure
{
    public class Configuration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IOwnerCarService, OwnerCarService>();

        }
    }
}