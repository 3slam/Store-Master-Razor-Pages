using StoreMaster.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreMaster.DataAccess.Repository.IRepository;
using StoreMaster.DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StoreMaster.Models;
using Microsoft.AspNetCore.Identity;
using Stripe;
using StoreMaster.Utility;


namespace CompanyProjectWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddDbContext<DBContext>(options =>
             {
                 options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
             });

             builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DBContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
 
                options.LoginPath = new PathString("/Account/Login");
            
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

          
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

			app.UseRouting();

            app.UseAuthorization();

            app.UseSession();
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
