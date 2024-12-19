
using Microsoft.EntityFrameworkCore;
using TechnicalChallenge.SchoolManagement.Data;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Models;
using TechnicalChallenge.SchoolManagement.Presenters.Presenters;
using TechnicalChallenge.SchoolManagement.Presenters.Student;
using TechnicalChallenge.SchoolManagement.Repository;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;
using TechnicalChallenge.SchoolManagement.UseCases.Student;

namespace TechnicalChallenge.SchoolManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            // Dependencies
            builder.Services.AddScoped<IRepository<Student>, StudentRepository>();

            // Use Cases
            builder.Services.AddScoped<GetStudentByIdUseCase<Student, StudentViewModel>>();
            builder.Services.AddScoped<GetAllStudentUseCase<Student, StudentViewModel>>();
            builder.Services.AddScoped<CreateStudentUseCase>();

            // Presenters
            builder.Services.AddScoped<IPresenter<Student, StudentViewModel>, StudentPresenter>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "_corsConfiguration",
                                  builder =>
                                  {
                                      builder
                                             .AllowAnyOrigin()
                                             .AllowAnyMethod()
                                             .AllowAnyHeader();
                                  });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Automatically apply migrations on startup
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("_corsConfiguration");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
