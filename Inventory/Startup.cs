using Inventory.DAL;
using Inventory.DAL.Entities;
using Inventory.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Inventory
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            })
             .AddEntityFrameworkStores<DataContext>()
             .AddDefaultTokenProviders();

            services.AddCustomServices();
           

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseHttpContext();
            app.UseStaticFiles();
            app.RegisterStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/Account/Login");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "MyArea",pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
