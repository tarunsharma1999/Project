using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PensionManagementPortal.Migrations.DbHelperMigrations
{
    public partial class local_data_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOFWithdraw",
                table: "pension",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOFWithdraw",
                table: "pension");
        }
    }
}
