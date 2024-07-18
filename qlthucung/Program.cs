using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using qlthucung.Models;
using qlthucung.Security;
using qlthucung.Services;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

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
                        // Add services to the container
                        services.AddControllersWithViews()
                            .AddNewtonsoftJson(options =>
                            {
                                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                            });

                        services.AddControllers();

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

                        // Add Swagger services
                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();
                    });

                    webBuilder.Configure((context, app) =>
                    {
                        var env = context.HostingEnvironment;
                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                            app.UseSwagger();
                            app.UseSwaggerUI(c =>
                            {
                                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                            });
                        }
                        else
                        {
                            app.UseExceptionHandler("/Home/Error");
                            app.UseHsts();
                        }

                        app.UseHttpsRedirection();
                        app.UseStaticFiles();

                        app.UseSession();

                        app.UseRouting();

                        app.UseAuthentication();

                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=SanPham}/{action=Index}/{id?}");

                            // Map API controllers
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
