using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Models;

namespace TechnicalChallenge.SchoolManagement.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<StudentModel> Students { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<GradeModel> Grades { get; set; }
        public DbSet<GenderModel> Genders { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<StudentGradeModel> StudentGrade { get; set; }
        public DbSet<TeacherAssignmentModel> TeacherAssignment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentModel>().ToTable("Student");
            modelBuilder.Entity<TeacherModel>().ToTable("Teacher");
            modelBuilder.Entity<GradeModel>().ToTable("Grade");
            modelBuilder.Entity<GenderModel>().ToTable("Gender");
            modelBuilder.Entity<GroupModel>().ToTable("Group");
            modelBuilder.Entity<TeacherAssignmentModel>().ToTable("TeacherAssignment");
            modelBuilder.Entity<StudentGradeModel>().ToTable("StudentGrade");

            // One-to-one relationship: Student -> StudentGradeModel
            // A Student has one StudentGradeModel
            // StudentGradeModel has a foreign key StudentId
            modelBuilder.Entity<StudentModel>()
                .HasOne(s => s.StudentGrade)
                .WithOne(sg => sg.Student)
                .HasForeignKey<StudentGradeModel>(sg => sg.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-one relationship: StudentGradeModel -> Grade
            modelBuilder.Entity<StudentGradeModel>()
                .HasOne(sg => sg.Grade)
                .WithMany(g => g.StudentGrades)
                .HasForeignKey(sg => sg.GradeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-one relationship: StudentGradeModel -> Group
            modelBuilder.Entity<StudentGradeModel>()
                .HasOne(sg => sg.Group)
                .WithMany(grp => grp.StudentGrades)
                .HasForeignKey(sg => sg.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // Composite unique key for StudentGradeModel: A Student cannot belong to the same Grade more than once,
            // even with a different Group. Prevents duplicates (StudentId, GradeId, GroupId)
            modelBuilder.Entity<StudentGradeModel>()
                .HasIndex(sg => new { sg.StudentId, sg.GradeId, sg.GroupId })
                .IsUnique();

            // Many-to-one relationship: TeacherAssignmentModel -> Teacher
            modelBuilder.Entity<TeacherAssignmentModel>()
                .HasOne(ta => ta.Teacher)
                .WithMany(t => t.TeacherAssignments)
                .HasForeignKey(ta => ta.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-one relationship: TeacherAssignmentModel -> Grade
            modelBuilder.Entity<TeacherAssignmentModel>()
                .HasOne(ta => ta.Grade)
                .WithMany(g => g.TeacherAssignments)
                .HasForeignKey(ta => ta.GradeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-one relationship: TeacherAssignmentModel -> Group
            modelBuilder.Entity<TeacherAssignmentModel>()
                .HasOne(ta => ta.Group)
                .WithMany(grp => grp.TeacherAssignments)
                .HasForeignKey(ta => ta.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // Composite unique key for TeacherAssignmentModel to prevent duplicates (TeacherId, GradeId, GroupId)
            modelBuilder.Entity<TeacherAssignmentModel>()
                .HasIndex(ta => new { ta.TeacherId, ta.GradeId, ta.GroupId })
                .IsUnique();

            // Many-to-one relationship: Grade -> Teacher
            modelBuilder.Entity<GradeModel>()
                .HasOne(g => g.Teacher)
                .WithMany(t => t.Grades)
                .HasForeignKey(g => g.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-one relationship: Student -> Gender
            modelBuilder.Entity<StudentModel>()
                .HasOne(s => s.Gender)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-one relationship: Teacher -> Gender
            modelBuilder.Entity<TeacherModel>()
                .HasOne(t => t.Gender)
                .WithMany(g => g.Teachers)
                .HasForeignKey(t => t.GenderId)
                .OnDelete(DeleteBehavior.Restrict);
            
            base.OnModelCreating(modelBuilder);

        }
    }
}
