using Mentoring_project.Business.Interfaces;
using Mentoring_project.Business.Services;
using Mentoring_project.Domain.Interfaces.Repositories;
using Mentoring_project.Infrastructure.Data.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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