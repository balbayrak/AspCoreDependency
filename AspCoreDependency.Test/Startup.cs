using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreDependency.Core.Concrete;
using AspCoreDependency.Core.Configuration;
using AspCoreDependency.Test.Abstract;
using AspCoreDependency.Test.Concrete.NameSpace1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspCoreDependency.Test
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
            services.AddControllersWithViews();

            //if namespace is null IServiceA inject with last concrete class.
            
            services.AutoBind();

            //services.AutoBind(option =>
            //{
            //    option.namespaceStr = "AspCoreDependency.Test.Concrete.NameSpace1";
            //});

            services.BindTransientByName<IService>()
                    .Add("serviceA", typeof(ManagerA))
                    .Add("serviceB", typeof(ManagerB))
                    .Add("serviceC", typeof(ManagerC))
                    .Build();



            DependencyResolver.Init(BuildServices(services));

        }

        private ServiceProvider BuildServices(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
