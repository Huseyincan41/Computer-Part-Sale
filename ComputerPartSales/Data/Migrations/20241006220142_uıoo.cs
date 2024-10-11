using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class uıoo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPopulated",
                table: "ComputerParts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ComputerParts",
                keyColumn: "ComputerPartId",
                keyValue: 1,
                column: "IsPopulated",
                value: false);

            migrationBuilder.UpdateData(
                table: "ComputerParts",
                keyColumn: "ComputerPartId",
                keyValue: 2,
                column: "IsPopulated",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPopulated",
                table: "ComputerParts");
        }
    }
}
