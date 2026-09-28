using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialBackendApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedToFileHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "FileHeaders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated",
                table: "FileHeaders");
        }
    }
}
