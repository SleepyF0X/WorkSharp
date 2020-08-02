using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WorkSharp.DAL;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.Mappers;
using WorkSharp.DAL.EFCoreRepository;

namespace WorkSharp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Connection");
            services.AddDbContext<WorkSharpDbContext>(options => options.UseSqlServer(connectionString));
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddMvc();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddIdentity<DbUser, IdentityRole>()
                .AddEntityFrameworkStores<WorkSharpDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}
