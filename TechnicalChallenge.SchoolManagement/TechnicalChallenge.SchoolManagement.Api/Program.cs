
using Microsoft.EntityFrameworkCore;
using TechnicalChallenge.SchoolManagement.Data;
using TechnicalChallenge.SchoolManagement.Dto.Grade;
using TechnicalChallenge.SchoolManagement.Dto.GradeGroup;
using TechnicalChallenge.SchoolManagement.Dto.Group;
using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.Dto.Teacher;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Mappers;
using TechnicalChallenge.SchoolManagement.Presenters.Presenters;
using TechnicalChallenge.SchoolManagement.Presenters.Student;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.Repository;
using TechnicalChallenge.SchoolManagement.UseCases.Gender;
using TechnicalChallenge.SchoolManagement.UseCases.Grade;
using TechnicalChallenge.SchoolManagement.UseCases.GradeGroup;
using TechnicalChallenge.SchoolManagement.UseCases.Group;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;
using TechnicalChallenge.SchoolManagement.UseCases.Student;
using TechnicalChallenge.SchoolManagement.UseCases.Teacher;

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
            builder.Services.AddScoped<IRepository<Teacher>, TeacherRepository>();
            builder.Services.AddScoped<IRepository<StudentGradeGroup>, StudentGradeGroupRepository>();
            builder.Services.AddScoped<IRepository<Group>, GroupRepository>();
            builder.Services.AddScoped<IRepository<Grade>, GradeRepository>();
            builder.Services.AddScoped<IRepository<Gender>, GenderRepository>();
            builder.Services.AddScoped<IRepository<GradeGroup>, GradeGroupRepository>();
            builder.Services.AddScoped<IRepository<TeacherGradeGroupClassAssignment>, TeacherGradeGroupClassAssignmentRespository>();
            builder.Services.AddScoped<IRepository<TeacherGradeGroupOwnership>, TeacherGradeGroupOwnershipAssignmentRepository>();

            // Use Cases

            //  Student
            builder.Services.AddScoped<GetStudentByIdUseCase<Student, StudentViewModel>>();
            builder.Services.AddScoped<GetAllStudentUseCase<Student, StudentViewModel>>();
            builder.Services.AddScoped<CreateStudentUseCase<CreateStudentRequestDto>>();
            builder.Services.AddScoped<UpdateStudentUseCase<UpdateStudentRequestDto>>();
            builder.Services.AddScoped<DeleteStudentUseCase<Student>>();
            builder.Services.AddScoped<AddStudentToGradeGroupUseCase<AddStudentToGradeGroupRequestDto>>();

            ////  Teacher
            builder.Services.AddScoped<GetTeacherByIdUseCase<Teacher, TeacherViewModel>>();
            builder.Services.AddScoped<GetAllTeachersUseCase<Teacher, TeacherViewModel>>();
            builder.Services.AddScoped<CreateTeacherUseCase<CreateTeacherRequestDto>>();
            builder.Services.AddScoped<UpdateTeacherUseCase<UpdateTeacherRequestDto>>();
            builder.Services.AddScoped<DeleteTeacherUseCase<Teacher>>();
            builder.Services.AddScoped<AssignTeacherToGradeGroupClassUseCase<AssignTeacherToGradeGroupClassRequestDto>>();
            builder.Services.AddScoped<AssignTeacherToGradeGroupOwnershipUseCase<AssignTeacherToGradeGroupOwnershipRequestDto>>();

            //  Gender
            builder.Services.AddScoped<GetAllGendersUseCase<Gender, GenderViewModel>>();

            //  Grade
            builder.Services.AddScoped<GetGradeByIdUseCase<Grade, GradeViewModel>>();
            builder.Services.AddScoped<GetAllGradesUseCase<Grade, GradeViewModel>>();
            builder.Services.AddScoped<CreateGradeUseCase<CreateGradeRequestDto>>();
            builder.Services.AddScoped<UpdateGradeUseCase<UpdateGradeRequestDto>>();
            builder.Services.AddScoped<DeleteGradeUseCase<Grade>>();

            //  Group
            builder.Services.AddScoped<GetGroupByIdUseCase<Group, GroupViewModel>>();
            builder.Services.AddScoped<GetAllGroupsUseCase<Group, GroupViewModel>>();
            builder.Services.AddScoped<CreateGroupUseCase<CreateGroupRequestDto>>();
            builder.Services.AddScoped<UpdateGroupUseCase<UpdateGroupRequestDto>>();
            builder.Services.AddScoped<DeleteGroupUseCase<Group>>();

            //  GradeGroup
            builder.Services.AddScoped<GetGradeGroupByIdUseCase<GradeGroup, GradeGroupViewModel>>();
            builder.Services.AddScoped<GetAllGradeGroupsUseCase<GradeGroup, GradeGroupViewModel>>();
            builder.Services.AddScoped<CreateGradeGroupUseCase<CreateGradeGroupRequestDto>>();
            builder.Services.AddScoped<UpdateGradeGroupUseCase<UpdateGradeGroupRequestDto>>();
            builder.Services.AddScoped<DeleteGradeGroupUseCase<GradeGroup>>();


            // Mapper
            builder.Services.AddScoped<IMapper<CreateGradeRequestDto, Grade>, CreateGradeMapper>();
            builder.Services.AddScoped<IMapper<UpdateGradeRequestDto, Grade>, UpdateGradeMapper>();

            builder.Services.AddScoped<IMapper<CreateGroupRequestDto, Group>, CreateGroupMapper>();
            builder.Services.AddScoped<IMapper<UpdateGroupRequestDto, Group>, UpdateGroupMapper>();

            builder.Services.AddScoped<IMapper<CreateGradeGroupRequestDto, GradeGroup>, CreateGradeGroupMapper>();
            builder.Services.AddScoped<IMapper<UpdateGradeGroupRequestDto, GradeGroup>, UpdateGradeGroupMapper>();

            builder.Services.AddScoped<IMapper<CreateStudentRequestDto, Student>, CreateStudentMapper>();
            builder.Services.AddScoped<IMapper<UpdateStudentRequestDto, Student>, UpdateStudentMapper>();            
            builder.Services.AddScoped<IMapper<AddStudentToGradeGroupRequestDto, StudentGradeGroup>, AddStudentToGradeGroupMapper>();

            builder.Services.AddScoped<IMapper<CreateTeacherRequestDto, Teacher>, CreateTeacherMapper>();
            builder.Services.AddScoped<IMapper<UpdateTeacherRequestDto, Teacher>, UpdateTeacherMapper>();
            builder.Services.AddScoped<IMapper<AssignTeacherToGradeGroupClassRequestDto, TeacherGradeGroupClassAssignment>, AssignTeacherToGradeGroupClassMapper>();
            builder.Services.AddScoped<IMapper<AssignTeacherToGradeGroupOwnershipRequestDto, TeacherGradeGroupOwnership>, AssignTeacherToGradeGroupOwnershipMapper>();

            // Presenters
            builder.Services.AddScoped<IPresenter<Student, StudentViewModel>, StudentPresenter>();
            builder.Services.AddScoped<IPresenter<Teacher, TeacherViewModel>, TeacherPresenter>();
            builder.Services.AddScoped<IPresenter<Group, GroupViewModel>, GroupPresenter>();
            builder.Services.AddScoped<IPresenter<Grade, GradeViewModel>, GradePresenter>();
            builder.Services.AddScoped<IPresenter<Gender, GenderViewModel>, GenderPresenter>();
            builder.Services.AddScoped<IPresenter<GradeGroup, GradeGroupViewModel>, GradeGroupPresenter>();


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
