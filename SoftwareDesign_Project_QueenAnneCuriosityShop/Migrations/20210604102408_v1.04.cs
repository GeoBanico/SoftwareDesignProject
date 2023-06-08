using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop.Migrations
{
    public partial class v104 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_BoughtStatuses_BoughtStatusId",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "BoughtStatusId",
                table: "Units",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_BoughtStatuses_BoughtStatusId",
                table: "Units",
                column: "BoughtStatusId",
                principalTable: "BoughtStatuses",
                principalColumn: "BoughtStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_BoughtStatuses_BoughtStatusId",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "BoughtStatusId",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_BoughtStatuses_BoughtStatusId",
                table: "Units",
                column: "BoughtStatusId",
                principalTable: "BoughtStatuses",
                principalColumn: "BoughtStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
