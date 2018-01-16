using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace test
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
            services.AddMvc();
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            //// Add Entity Framework services for SQL Server.
            //services.AddEntityFrameworkSqlServer()
                    //.AddDbContext<TodoAppContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:TodoAppDB"]));

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
                app.UseExceptionHandler("/Home/Error");
            }

            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            ////Drop and recreate the database at each run
            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    TodoAppContext ctx = serviceScope.ServiceProvider.GetService<TodoAppContext>();

            //    // Drop & recreate the database at each run (we're not using EF DB migrations)
            //    ctx.Database.EnsureDeleted();
            //    ctx.Database.EnsureCreated();

            //   
            //    ctx.EnsureSeedData();
            //}

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
