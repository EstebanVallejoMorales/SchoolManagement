
using Microsoft.EntityFrameworkCore;
using TechnicalChallenge.SchoolManagement.Data;
using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Mappers;
using TechnicalChallenge.SchoolManagement.Models;
using TechnicalChallenge.SchoolManagement.Presenters.Presenters;
using TechnicalChallenge.SchoolManagement.Presenters.Student;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
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

            //Repositories
            builder.Services.AddScoped<IRepository<Student>, StudentRepository>();
            builder.Services.AddScoped<IRepository<StudentGradeGroup>, StudentGradeGroupRepository>();
            builder.Services.AddScoped<IRepository<Group>, GroupRepository>();

            // Use Cases

            //  Student
            builder.Services.AddScoped<GetStudentByIdUseCase<Student, StudentViewModel>>();
            builder.Services.AddScoped<GetAllStudentUseCase<Student, StudentViewModel>>();
            builder.Services.AddScoped<CreateStudentUseCase<CreateStudentRequestDto>>();
            builder.Services.AddScoped<UpdateStudentUseCase<UpdateStudentRequestDto>>();
            builder.Services.AddScoped<DeleteStudentUseCase<Student>>();
            builder.Services.AddScoped<AddStudentToGradeGroupUseCase<AddStudentToGradeGroupRequestDto>>();

            ////  Teacher
            //builder.Services.AddScoped<GetStudentByIdUseCase<Student, StudentViewModel>>();
            //builder.Services.AddScoped<GetAllStudentUseCase<Student, StudentViewModel>>();
            //builder.Services.AddScoped<CreateStudentUseCase<CreateStudentRequestDto>>();
            //builder.Services.AddScoped<UpdateStudentUseCase<UpdateStudentRequestDto>>();
            //builder.Services.AddScoped<DeleteStudentUseCase<Student>>();
            //builder.Services.AddScoped<AddStudentToGradeGroupUseCase<AddStudentToGradeGroupRequestDto>>();

            ////  Grade
            //builder.Services.AddScoped<GetStudentByIdUseCase<Student, StudentViewModel>>();
            //builder.Services.AddScoped<GetAllStudentUseCase<Student, StudentViewModel>>();
            //builder.Services.AddScoped<CreateStudentUseCase<CreateStudentRequestDto>>();
            //builder.Services.AddScoped<UpdateStudentUseCase<UpdateStudentRequestDto>>();
            //builder.Services.AddScoped<DeleteStudentUseCase<Student>>();

            ////  Group
            //builder.Services.AddScoped<GetStudentByIdUseCase<Student, StudentViewModel>>();
            builder.Services.AddScoped<GetAllGroupsUseCase<Group, GroupViewModel>>();
            //builder.Services.AddScoped<CreateStudentUseCase<CreateStudentRequestDto>>();
            //builder.Services.AddScoped<UpdateStudentUseCase<UpdateStudentRequestDto>>();
            //builder.Services.AddScoped<DeleteStudentUseCase<Student>>();


            // Mapper
            builder.Services.AddScoped<IMapper<CreateStudentRequestDto, Student>, CreateStudentMapper>();
            builder.Services.AddScoped<IMapper<UpdateStudentRequestDto, Student>, UpdateStudentMapper>();
            builder.Services.AddScoped<IMapper<AddStudentToGradeGroupRequestDto, StudentGradeGroup>, AddStudentToGradeGroupMapper>();

            // Presenters
            builder.Services.AddScoped<IPresenter<Student, StudentViewModel>, StudentPresenter>();
            builder.Services.AddScoped<IPresenter<Group, GroupViewModel>, GroupPresenter>();


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
