using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalChallenge.SchoolManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewRelationShipsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Teacher_TeacherId",
                table: "Grade");

            migrationBuilder.DropTable(
                name: "StudentGrade");

            migrationBuilder.DropTable(
                name: "TeacherAssignment");

            migrationBuilder.DropIndex(
                name: "IX_Grade_TeacherId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Grade");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Student",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                name: "IX_GradeGroup_GradeId_GroupId",
                table: "GradeGroup",
                columns: new[] { "GradeId", "GroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GradeGroup_GroupId",
                table: "GradeGroup",
                column: "GroupId");

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
                name: "GradeGroup");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StudentGrade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGrade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGrade_Grade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentGrade_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentGrade_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherAssignment_Grade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherAssignment_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherAssignment_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grade_TeacherId",
                table: "Grade",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrade_GradeId",
                table: "StudentGrade",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrade_GroupId",
                table: "StudentGrade",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrade_StudentId",
                table: "StudentGrade",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrade_StudentId_GradeId_GroupId",
                table: "StudentGrade",
                columns: new[] { "StudentId", "GradeId", "GroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_GradeId",
                table: "TeacherAssignment",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_GroupId",
                table: "TeacherAssignment",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_TeacherId_GradeId_GroupId",
                table: "TeacherAssignment",
                columns: new[] { "TeacherId", "GradeId", "GroupId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Teacher_TeacherId",
                table: "Grade",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
