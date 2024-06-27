using ContactManager.Repository.Implementation;
using ContactManager.Repository.Interface;
using ContactManager.Service.Implementation;
using ContactManager.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Service
{
    public static class ConfigureServiceContainer
    {
        public static void AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IContactService, ContactService>();
        }
    }
}
