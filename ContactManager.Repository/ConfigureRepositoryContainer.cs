using ContactManager.Repository.Implementation;
using ContactManager.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Repository
{
    public static class ConfigureRepositoryContainer
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
        }
 
    }
}
