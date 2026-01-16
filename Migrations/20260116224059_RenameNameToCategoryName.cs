using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contoso_receipt_backend.Migrations
{
    /// <inheritdoc />
    public partial class RenameNameToCategoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Categories_Name",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "ChangedAt",
                table: "StatusChanges");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Receipts",
                newName: "CategoryName");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_Name",
                table: "Receipts",
                newName: "IX_Receipts_CategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Categories_CategoryName",
                table: "Receipts",
                column: "CategoryName",
                principalTable: "Categories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Categories_CategoryName",
                table: "Receipts");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Receipts",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_CategoryName",
                table: "Receipts",
                newName: "IX_Receipts_Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangedAt",
                table: "StatusChanges",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Categories_Name",
                table: "Receipts",
                column: "Name",
                principalTable: "Categories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
