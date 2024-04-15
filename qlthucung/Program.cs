using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using qlthucung.Models;
using qlthucung.Security;
using qlthucung.Services;

namespace qlthucung
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        // Your ConfigureServices code goes here
                        services.AddControllersWithViews();
                        services.AddDbContext<AppDbContext>(options => options.UseSqlServer
                            (context.Configuration.GetConnectionString("AppDb")));

                        services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("AppDb")));

                        services.AddIdentity<AppIdentityUser, AppIdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();

                        services.ConfigureApplicationCookie(options =>
                        {
                            options.LoginPath = "/Security/SignIn";
                            options.AccessDeniedPath = "/Security/AccessDenied";
                        });

                        services.AddSession();
                        services.AddScoped<IVnPayService, VnPayService>();
                    });

                    webBuilder.Configure((context, app) =>
                    {
                        var env = context.HostingEnvironment;
                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }

                        app.UseSession();

                        app.UseStaticFiles();

                        app.UseRouting();

                        app.UseAuthentication();

                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=SanPham}/{action=Index}/{id?}");
                        });
                    });
                });
    }
}
