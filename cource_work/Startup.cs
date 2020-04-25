using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using cource_work.Models.Entity;
using Microsoft.EntityFrameworkCore;
using cource_work.Models;
using IApplicationLifetime = Microsoft.AspNetCore.Hosting.IApplicationLifetime;
using cource_work.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace cource_work
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("connectingString");
            services.AddDbContext<bus_stationContext>(options => options.UseSqlServer(connection));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options => 
                {
                   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                   options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            services.AddAuthorization();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMiddleware<WorkPreparatorMiddleware>(Configuration.GetConnectionString("connectingString"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            lifetime.ApplicationStarted.Register(new WorkPreparator(Configuration.GetConnectionString("connectingString")).createJourneysAndCashAmount);
            lifetime.ApplicationStarted.Register(new WorkEnder(Configuration.GetConnectionString("connectingString")).collectTransactions);
        }

        

        
    }

    
}
