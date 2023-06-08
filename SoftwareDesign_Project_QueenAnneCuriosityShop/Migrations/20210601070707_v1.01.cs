using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop.Migrations
{
    public partial class v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "SaleUnits");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "SaleUnits");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StoreCities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OnlineStores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Onlines",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StoreCities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OnlineStores");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Onlines");

            migrationBuilder.AddColumn<float>(
                name: "Subtotal",
                table: "SaleUnits",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Tax",
                table: "SaleUnits",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
