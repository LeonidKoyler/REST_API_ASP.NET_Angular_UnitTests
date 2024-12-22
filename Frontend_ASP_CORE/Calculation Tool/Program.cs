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
                client.BaseAddress = new Uri(url + "/vehicle");
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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

        private static string GetUri()
        {
            var isRunningInContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != null;
            return isRunningInContainer ? "http://backend/api" : "https://localhost:7198/api";
        }
    }
}
