using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using CRM.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using CRM.DataAccess.Interfaces;
using CRM.BusinessLayer;
using CRM.BusinessLayer.Department;

namespace clientRelationshipManagement
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSpaStaticFiles(c=> { c.RootPath = "ClientApp/dist"; });
            services.Configure<Settings>( options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });
            // Dependency Injection
            services.AddTransient<IUsersManager, UsersManager>();
            services.AddTransient<IDepartmentManager, DepartmentManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc(routes => {
                routes.MapRoute(name: "default", template:"{controller}/{action=index}/{id}");
            });
            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript:"start");
                }
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
