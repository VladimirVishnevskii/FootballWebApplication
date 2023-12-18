using FootballWebApplication;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // получаем строку подключения из файла конфигурации
        ////string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

        // добавляем контекст ApplicationContext в качестве сервиса в приложение
        ////builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        //Исправляет ошибку Cannot write DateTime with Kind=Local to PostgreSQL type 'timestamp with time zone', only UTC is supported.
        //Note that it's not possible to mix DateTimes with different Kinds in an array/range.
        //See the Npgsql.EnableLegacyTimestampBehavior AppContext switch to enable legacy behavior.
        //System.InvalidCastException: Cannot write DateTime with Kind=Local to PostgreSQL type 'timestamp with time zone', only UTC is supported.
        //Note that it's not possible to mix DateTimes with different Kinds in an array/range.
        //See the Npgsql.EnableLegacyTimestampBehavior AppContext switch to enable legacy behavior.
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Title}/{id?}");

        app.Run();
    }
}