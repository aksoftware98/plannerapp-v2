using Microsoft.Extensions.DependencyInjection;
using PlaneerApp.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneerApp.Client.Services
{
    public static class DependencyInjectionExtensions
    {

        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            return services.AddScoped<IAuthenticationService, HttpAuthenticationService>()
                           .AddScoped<IPlansService, HttpPlansService>();
        }

    }
}
