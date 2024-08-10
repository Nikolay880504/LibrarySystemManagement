using LibrarySystemManagement.Data;
using System.Configuration;
using Microsoft.Extensions.Configuration.Json;
using LibrarySystemManagement.Data.Connection;
using LibrarySystemManagement.Data.Initializer;
using System.Data.SqlClient;

namespace LibrarySystemManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var configurationBuilder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = configurationBuilder.Build();

            var connectionString = configuration.GetConnectionString("Connection");
            builder.Services.AddControllersWithViews();
            
             if (connectionString != null)
             {
                var databaseInitializer = new SqlDatabaseInitializer(connectionString);
                 databaseInitializer.InitializeDatabase();

                 builder.Services.AddScoped<IDatabaseConnection>
                (provider => new SqlDataBaseConnection(connectionString));
             }

            builder.Services.AddScoped<IBookRepository, SqlBookRepository>();
            builder.Services.AddScoped<IReaderRepository, SqlReaderRepository>();
            builder.Services.AddScoped<IBorrowingRepository, SqlBorrowingRepository>();
            builder.Services.AddScoped<ICategoryRepository, SqlCaregoryRepository>();
            builder.Services.AddScoped<IBookInstanceRepository, SqlBookInctanceRepository>();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
       
            }
        }
      
            
        
    
