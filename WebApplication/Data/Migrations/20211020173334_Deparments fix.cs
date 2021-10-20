using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Data.Migrations
{
    public partial class Deparmentsfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Faculties_FacultyId1",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_FacultyId1",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "FacultyId1",
                table: "Departments");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                column: "FacultyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "FacultyId1",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OverviewDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmploymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    FacultyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Financing = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LectureId = table.Column<int>(type: "int", nullable: true),
                    LectureRoom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Premium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverviewDto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                column: "FacultyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId1",
                table: "Departments",
                column: "FacultyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Faculties_FacultyId1",
                table: "Departments",
                column: "FacultyId1",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
