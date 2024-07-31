using AppServices.Dates.Repositories;
using AppServices.Dates.Services;
using AutoMapper;
using ComponentRegistrar.MapProfiles;
using DataAccess;
using DataAccess.Dates;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentRegistrar
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddRazorPages();


            services.AddTransient<IDateService, DateService>();
            services.AddScoped<IDatesRepository, DatesRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
            services.AddScoped<DbContext>(s => s.GetRequiredService<ApplicationDbContext>());
            return services.ConfigureAutoMapper();
        }

        private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            return services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DateProfile>();
            });
            config.AssertConfigurationIsValid();
            return config;
        }
    }
}
