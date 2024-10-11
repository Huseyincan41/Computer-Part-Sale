using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ooopp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_computerPartSaleDetails_ComputerPartSaleId",
                table: "computerPartSaleDetails");

            migrationBuilder.CreateIndex(
                name: "IX_computerPartSaleDetails_ComputerPartSaleId",
                table: "computerPartSaleDetails",
                column: "ComputerPartSaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_computerPartSaleDetails_ComputerPartSaleId",
                table: "computerPartSaleDetails");

            migrationBuilder.CreateIndex(
                name: "IX_computerPartSaleDetails_ComputerPartSaleId",
                table: "computerPartSaleDetails",
                column: "ComputerPartSaleId",
                unique: true);
        }
    }
}
