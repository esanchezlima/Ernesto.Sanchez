using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovePais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
