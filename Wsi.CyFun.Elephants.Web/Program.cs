using Microsoft.EntityFrameworkCore;
using Wsi.CyFun.Elephants.Web.Data;
using Wsi.CyFun.Elephants.Web.Services;

namespace Wsi.CyFun.Elephants.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            // Add HttpContextAccessor - register this before session services
            builder.Services.AddHttpContextAccessor();
            
            
            // Session configuratie
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = "Wsi.CyFun.Elephants.Session";
            });
            
            builder.Services.AddScoped<IAdminAddDataService, AdminAddDataService>();
            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ElephantCyFunDb")));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}