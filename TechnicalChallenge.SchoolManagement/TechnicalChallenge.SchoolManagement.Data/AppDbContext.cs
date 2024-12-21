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
        public DbSet<GradeGroupModel> GradeGroups { get; set; }
        public DbSet<StudentGradeGroupModel> StudentGradeGroups { get; set; }
        public DbSet<TeacherGradeGroupOwnershipModel> TeacherGradeGroupOwnerships { get; set; }
        public DbSet<TeacherGradeGroupClassAssignmentModel> TeacherGradeGroupClassAssignments { get; set; }

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
            modelBuilder.Entity<GradeGroupModel>().ToTable("GradeGroup");
            modelBuilder.Entity<StudentGradeGroupModel>().ToTable("StudentGradeGroup");
            modelBuilder.Entity<TeacherGradeGroupClassAssignmentModel>().ToTable("TeacherGradeGroupClassAssignment");
            modelBuilder.Entity<TeacherGradeGroupOwnershipModel>().ToTable("TeacherGradeGroupOwnership");

            // Student -> Gender
            modelBuilder.Entity<StudentModel>()
                .HasOne(s => s.Gender)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Teacher -> Gender
            modelBuilder.Entity<TeacherModel>()
                .HasOne(t => t.Gender)
                .WithMany(g => g.Teachers)
                .HasForeignKey(t => t.GenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // GradeGroup: Composite Key
            modelBuilder.Entity<GradeGroupModel>()
                .HasIndex(gg => new { gg.GradeId, gg.GroupId })
                .IsUnique();

            // StudentGradeGroup: Composite Key
            modelBuilder.Entity<StudentGradeGroupModel>()
                .HasIndex(sgg => new { sgg.StudentId, sgg.GradeGroupId })
                .IsUnique();

            // Relationships

            // GradeGroup -> Grade
            modelBuilder.Entity<GradeGroupModel>()
                .HasOne(gg => gg.Grade)
                .WithMany(g => g.GradeGroups)
                .HasForeignKey(gg => gg.GradeId)
                .OnDelete(DeleteBehavior.Restrict);

            // GradeGroup -> Group
            modelBuilder.Entity<GradeGroupModel>()
                .HasOne(gg => gg.Group)
                .WithMany(grp => grp.GradeGroups)
                .HasForeignKey(gg => gg.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentGradeGroup -> Student
            modelBuilder.Entity<StudentGradeGroupModel>()
                .HasOne(sgg => sgg.Student)
                .WithMany(s => s.StudentGradeGroups)
                .HasForeignKey(sgg => sgg.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentGradeGroup -> GradeGroup
            modelBuilder.Entity<StudentGradeGroupModel>()
                .HasOne(sgg => sgg.GradeGroup)
                .WithMany(gg => gg.StudentGradeGroups)
                .HasForeignKey(sgg => sgg.GradeGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // TeacherGradeGroupOwnership -> Teacher
            modelBuilder.Entity<TeacherGradeGroupOwnershipModel>()
                .HasOne(tgo => tgo.Teacher)
                .WithMany(t => t.TeacherGradeGroupOwnerships)
                .HasForeignKey(tgo => tgo.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // TeacherGradeGroupOwnership -> GradeGroup
            modelBuilder.Entity<TeacherGradeGroupOwnershipModel>()
                .HasOne(tgo => tgo.GradeGroup)
                .WithMany(gg => gg.TeacherGradeGroupOwnerships)
                .HasForeignKey(tgo => tgo.GradeGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // TeacherGradeGroupOwnership: Composite Key
            modelBuilder.Entity<TeacherGradeGroupOwnershipModel>()
                .HasIndex(tgo => new { tgo.TeacherId, tgo.GradeGroupId })
                .IsUnique();

            // TeacherGradeGroupOwnership: Unique TeacherId
            modelBuilder.Entity<TeacherGradeGroupOwnershipModel>()
                .HasIndex(tgo => tgo.TeacherId)
                .IsUnique();

            // TeacherGradeGroupOwnership: Unique GradeGroupId
            modelBuilder.Entity<TeacherGradeGroupOwnershipModel>()
                .HasIndex(tgo => tgo.GradeGroupId)
                .IsUnique();

            // TeacherGradeGroupClassAssignment -> Teacher
            modelBuilder.Entity<TeacherGradeGroupClassAssignmentModel>()
                .HasOne(tgca => tgca.Teacher)
                .WithMany(t => t.TeacherGradeGroupClassAssignments)
                .HasForeignKey(tgca => tgca.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // TeacherGradeGroupClassAssignment -> GradeGroup
            modelBuilder.Entity<TeacherGradeGroupClassAssignmentModel>()
                .HasOne(tgca => tgca.GradeGroup)
                .WithMany(gg => gg.TeacherGradeGroupClassAssignments)
                .HasForeignKey(tgca => tgca.GradeGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // TeacherGradeGroupClassAssignment: Composite Key
            modelBuilder.Entity<TeacherGradeGroupClassAssignmentModel>()
                .HasIndex(tgca => new { tgca.TeacherId, tgca.GradeGroupId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);

        }
    }
}
