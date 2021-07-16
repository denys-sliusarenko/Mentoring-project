using MentoringProject.Application.Interfaces;
using MentoringProject.Application.Services;
using MentoringProject.Domain.Core.Repositories;
using MentoringProject.Infrastructure.Data.Data;
using Microsoft.Extensions.DependencyInjection;

namespace MentoringProject.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}