using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using SalesApp.Data;
using SalesApp.Services;
namespace SalesApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SalesAppContext>(
                options => options.UseMySql(builder.Configuration.GetConnectionString("SalesAppContext"), 
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SalesAppContext")), 
                builder => builder.MigrationsAssembly("SalesApp")));


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionStringMysql = builder.Configuration.GetConnectionString("SalesAppContext"); builder.Services.AddDbContext<SalesAppContext>(options => options.UseMySql(connectionStringMysql, ServerVersion.Parse("8.0.25-mysql")));

            builder.Services.AddScoped<SeedingServices>();
            builder.Services.AddScoped<SellerService>();
            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddScoped<SalesRecordService>();

            var app = builder.Build();
            app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingServices>().Seed();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            app.UseRequestLocalization(localizationOptions);

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //isso define que por padr�o a rota ser� no controlador Home e a a��o na pagina Index, o Id � opcional
            app.Run();
        }
    }
}