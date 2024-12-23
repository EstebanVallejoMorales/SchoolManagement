using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalChallenge.SchoolManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teacher_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GradeGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeGroup_Grade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GradeGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentGradeGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    GradeGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGradeGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGradeGroup_GradeGroup_GradeGroupId",
                        column: x => x.GradeGroupId,
                        principalTable: "GradeGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentGradeGroup_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherGradeGroupClassAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    GradeGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherGradeGroupClassAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherGradeGroupClassAssignment_GradeGroup_GradeGroupId",
                        column: x => x.GradeGroupId,
                        principalTable: "GradeGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherGradeGroupClassAssignment_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherGradeGroupOwnership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    GradeGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherGradeGroupOwnership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherGradeGroupOwnership_GradeGroup_GradeGroupId",
                        column: x => x.GradeGroupId,
                        principalTable: "GradeGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherGradeGroupOwnership_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Name",
                table: "Grade",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GradeGroup_GradeId_GroupId",
                table: "GradeGroup",
                columns: new[] { "GradeId", "GroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GradeGroup_GroupId",
                table: "GradeGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_Name",
                table: "Group",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_GenderId",
                table: "Student",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGradeGroup_GradeGroupId",
                table: "StudentGradeGroup",
                column: "GradeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGradeGroup_StudentId_GradeGroupId",
                table: "StudentGradeGroup",
                columns: new[] { "StudentId", "GradeGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_GenderId",
                table: "Teacher",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGradeGroupClassAssignment_GradeGroupId",
                table: "TeacherGradeGroupClassAssignment",
                column: "GradeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGradeGroupClassAssignment_TeacherId_GradeGroupId",
                table: "TeacherGradeGroupClassAssignment",
                columns: new[] { "TeacherId", "GradeGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGradeGroupOwnership_GradeGroupId",
                table: "TeacherGradeGroupOwnership",
                column: "GradeGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGradeGroupOwnership_TeacherId",
                table: "TeacherGradeGroupOwnership",
                column: "TeacherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGradeGroupOwnership_TeacherId_GradeGroupId",
                table: "TeacherGradeGroupOwnership",
                columns: new[] { "TeacherId", "GradeGroupId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentGradeGroup");

            migrationBuilder.DropTable(
                name: "TeacherGradeGroupClassAssignment");

            migrationBuilder.DropTable(
                name: "TeacherGradeGroupOwnership");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "GradeGroup");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Gender");
        }
    }
}
