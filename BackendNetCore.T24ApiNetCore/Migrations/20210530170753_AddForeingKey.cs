using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendNetCore.T24ApiNetCore.Migrations
{
    public partial class AddForeingKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Clientes_ClienteId",
                table: "Videos");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Clientes_ClienteId",
                table: "Videos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Clientes_ClienteId",
                table: "Videos");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Clientes_ClienteId",
                table: "Videos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
