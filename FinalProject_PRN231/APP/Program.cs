using APP.IServices;
using APP.Services;
using DataAccess.AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace APP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddTransient<IAccountServices, AccountServices>();
            builder.Services.AddTransient<IProductServices, ProductServices>();
            builder.Services.AddTransient<IOrderServices, OrderServices>();
            builder.Services.AddTransient<IReportServices, ReportServices>();
            builder.Services.AddAutoMapper(typeof(Program), typeof(AutoMapperConfiguration));
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.AccessDeniedPath = "/AccessDenied";
            });
            builder.Services.AddAuthorization();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.MapRazorPages();

            app.Run();
        }
    }
}