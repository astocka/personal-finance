using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinance.Web.Data;
using PersonalFinance.Web.Interfaces;
using PersonalFinance.Web.Models;
using PersonalFinance.Web.Services;
using PersonalFinance.Web.Services.Interfaces;

namespace PersonalFinance.Web
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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<DataContext>();

            services.AddScoped<ITransaction, Expense>();
            services.AddScoped<ITransaction, Revenue>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IRevenueService, RevenueService>();
            services.AddScoped<IBudgetService, BudgetService>();
            services.AddScoped<IBillService, BillService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "budget",
                   template: "{controller=Budget}/{budgetId?}/{action=Index}/{id?}");

                routes.MapRoute(
                  name: "budgetDetails",
                  template: "{controller=Budget}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
                
        }
    }
}
