using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace BackendNetCore.T24ApiNetCore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cientificos",
                columns: table => new
                {
                    DNI = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    NombreCompleto = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cientificos", x => x.DNI);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Apellido = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    DNI = table.Column<int>(type: "int", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Horas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Director = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AsignadoAs",
                columns: table => new
                {
                    CientificoId = table.Column<string>(type: "varchar(8)", nullable: false),
                    ProyectoId = table.Column<string>(type: "varchar(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignadoAs", x => new { x.ProyectoId, x.CientificoId });
                    table.ForeignKey(
                        name: "FK_AsignadoAs_Cientificos_CientificoId",
                        column: x => x.CientificoId,
                        principalTable: "Cientificos",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignadoAs_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignadoAs_CientificoId",
                table: "AsignadoAs",
                column: "CientificoId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ClienteId",
                table: "Videos",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignadoAs");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Cientificos");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
