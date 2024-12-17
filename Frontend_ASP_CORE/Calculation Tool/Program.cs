using Calculation_Tool.Service;

namespace Calculation_Tool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var url = GetUri();
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddHttpClient("vehicleApi", client =>
            {
                client.BaseAddress = new Uri(url);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.Run();
        }

        //  todo Getting url should be implemented in different way.
        // This is temporary solution for supporting docker containers and local run from Visual studio.
        private static string GetUri()
        {
            var isRunningFromVisualStudio = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == null &&
                                            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            return isRunningFromVisualStudio ? "https://localhost:7198/api/vehicle" : "http://backend/api/vehicle";
        }
    }
}
