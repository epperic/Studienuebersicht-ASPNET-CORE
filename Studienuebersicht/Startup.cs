using Culture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Studienuebersicht.Model;

namespace Studienuebersicht
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /*var redis_connection_string = Helper.GetFromEnvironmentOrDefault("REDIS_CONNECTION_STRING", "");
            if (redis_connection_string == string.Empty)
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                var redis = ConnectionMultiplexer.Connect(redis_connection_string);
                services.AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys")
                    .SetApplicationName("Studienuebersicht");

                services.AddStackExchangeRedisCache(o =>
                {
                    o.Configuration = redis_connection_string;
                    o.InstanceName = "Studienuebersicht";
                });
            }*/

            services.AddSession();
            services.AddControllersWithViews();
            services.AddSingleton<IRepository, MemoryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseCustomAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "semester",
                pattern: "Semester/{id}",
                defaults: new { controller = "Semester", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
