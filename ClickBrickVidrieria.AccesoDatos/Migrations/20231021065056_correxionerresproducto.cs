using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickBrickVidrieria.AccesoDatos.Migrations
{
    public partial class correxionerresproducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaId }",
                table: "Productos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId }",
                table: "Productos",
                type: "int",
                nullable: true);
        }
    }
}
