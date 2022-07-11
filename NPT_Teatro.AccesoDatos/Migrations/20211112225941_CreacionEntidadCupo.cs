using Microsoft.EntityFrameworkCore.Migrations;

namespace NPT_Teatro.AccesoDatos.Migrations
{
    public partial class CreacionEntidadCupo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cupo",
                table: "Funcion",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cupo",
                table: "Funcion");
        }
    }
}
