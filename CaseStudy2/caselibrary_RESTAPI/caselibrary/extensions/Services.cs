using caselibrary.Models;
using caselibrary.repos;


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LibProject.Extensions
{
    public static class Services
    {
        public static IServiceCollection AddCustomerLibrary(this IServiceCollection services, string Constr)
        {
            services.AddDbContext<RetailapplicationContext>(options => options.UseSqlServer(Constr));
            services.AddScoped<IUser,Usertable1>();
            return services;
        }
    }
}