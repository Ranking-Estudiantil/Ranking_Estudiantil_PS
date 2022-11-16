using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ranking_Estudiantil.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicUnity",
                columns: table => new
                {
                    AcademicUnityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicUnityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicUnity", x => x.AcademicUnityID);
                });

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    FacultyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.FacultyID);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FacultyID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_Department_Faculty_FacultyID",
                        column: x => x.FacultyID,
                        principalTable: "Faculty",
                        principalColumn: "FacultyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Career",
                columns: table => new
                {
                    CareerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Career", x => x.CareerID);
                    table.ForeignKey(
                        name: "FK_Career_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SecondLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AcademicUnityID = table.Column<int>(type: "int", nullable: false),
                    CareerID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Person_AcademicUnity_AcademicUnityID",
                        column: x => x.AcademicUnityID,
                        principalTable: "AcademicUnity",
                        principalColumn: "AcademicUnityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Person_Career_CareerID",
                        column: x => x.CareerID,
                        principalTable: "Career",
                        principalColumn: "CareerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Professor_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<byte>(type: "tinyint", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Student_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentsID = table.Column<int>(type: "int", nullable: false),
                    achievment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    punctuation = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentPersonID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectsID);
                    table.ForeignKey(
                        name: "FK_Projects_Student_StudentPersonID",
                        column: x => x.StudentPersonID,
                        principalTable: "Student",
                        principalColumn: "PersonID");
                });

            migrationBuilder.CreateTable(
                name: "Sanctions",
                columns: table => new
                {
                    SanctionsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentsID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    punctuation = table.Column<double>(type: "float", nullable: false),
                    StudentPersonID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanctions", x => x.SanctionsID);
                    table.ForeignKey(
                        name: "FK_Sanctions_Student_StudentPersonID",
                        column: x => x.StudentPersonID,
                        principalTable: "Student",
                        principalColumn: "PersonID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Career_DepartmentID",
                table: "Career",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Department_FacultyID",
                table: "Department",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_AcademicUnityID",
                table: "Person",
                column: "AcademicUnityID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CareerID",
                table: "Person",
                column: "CareerID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StudentPersonID",
                table: "Projects",
                column: "StudentPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Sanctions_StudentPersonID",
                table: "Sanctions",
                column: "StudentPersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Sanctions");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "AcademicUnity");

            migrationBuilder.DropTable(
                name: "Career");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Faculty");
        }
    }
}
