using MentoringProject.Application.Interfaces;
using MentoringProject.Application.Services;
using MentoringProject.Domain.Core.Repositories;
using MentoringProject.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace MentoringProject.Infrastructure
{
    public class Configuration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}