namespace TennisShopSystem.Web
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using TennisShopSystem.Web.Areas.Admin.Services;
    using TennisShopSystem.Web.Areas.Admin.Services.Interfaces;
    using Data;
    using Data.Models;
    using TennisShopSystem.Services.Data.Interfaces;
    using Infrastructure.Extensions;
    using Infrastructure.ModelBinders;

    using static Common.GeneralApplicationConstants;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString =
                builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<TennisShopDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<TennisShopDbContext>();

            builder.Services.AddApplicationServices(typeof(IProductService));
            builder.Services.AddScoped<IOrderService, OrderService>();

            //TODO: Research how you can register IOrderService with AddApplicationServices method?

            //TODO: Configure application cookie, check what recaptchaService is? Video: 31:44 Kris Last Workshop15.

            builder.Services
                .AddControllersWithViews()
                .AddMvcOptions(options =>
                {
                    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                });

            builder.Services.AddMemoryCache();
            builder.Services.AddSession();

            WebApplication app = builder.Build();

            app.UseSession();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.SeedAdministrator(DevelopmentAdminEmail);
            }

            app.UseEndpoints(config =>
            {
                config.MapControllerRoute(
                    name: "areas",
                    pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                config.MapDefaultControllerRoute();

                config.MapRazorPages();
            });

            app.Run();
        }
    }
}