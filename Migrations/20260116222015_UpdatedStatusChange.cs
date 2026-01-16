using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contoso_receipt_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStatusChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusChanges",
                table: "StatusChanges");

            migrationBuilder.AlterColumn<string>(
                name: "New_status",
                table: "StatusChanges",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Old_status",
                table: "StatusChanges",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StatusChanges",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangedAt",
                table: "StatusChanges",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusChanges",
                table: "StatusChanges",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StatusChanges_Email",
                table: "StatusChanges",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusChanges",
                table: "StatusChanges");

            migrationBuilder.DropIndex(
                name: "IX_StatusChanges_Email",
                table: "StatusChanges");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StatusChanges");

            migrationBuilder.DropColumn(
                name: "ChangedAt",
                table: "StatusChanges");

            migrationBuilder.AlterColumn<int>(
                name: "Old_status",
                table: "StatusChanges",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "New_status",
                table: "StatusChanges",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusChanges",
                table: "StatusChanges",
                columns: new[] { "Email", "Old_status", "New_status" });
        }
    }
}
