using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Order",
                type: "nvarchar(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Order");
        }
    }
}
