using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCRUD.Migrations
{
    public partial class MVCCONTEXT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Fecha = table.Column<DateTime>(type: "date", nullable: true),
                    Clave = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
