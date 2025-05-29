using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace db_relationship_proj.Migrations
{
    /// <inheritdoc />
    public partial class addedsetnullproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_ItemId",
                table: "SerialNumbers");

            migrationBuilder.AddColumn<int>(
                name: "SerialNumberId1",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "SerialNumberId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_ItemId",
                table: "SerialNumbers",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SerialNumberId1",
                table: "Items",
                column: "SerialNumberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SerialNumbers_SerialNumberId1",
                table: "Items",
                column: "SerialNumberId1",
                principalTable: "SerialNumbers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SerialNumbers_SerialNumberId1",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_ItemId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Items_SerialNumberId1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SerialNumberId1",
                table: "Items");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_ItemId",
                table: "SerialNumbers",
                column: "ItemId",
                unique: true,
                filter: "[ItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
