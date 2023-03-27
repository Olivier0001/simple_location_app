using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleLocation.DataAccess.Migrations
{
    public partial class addOrderHeaderAndDetailToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PickDateOfReturn",
                table: "OrderHeader",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PickUpDate",
                table: "OrderHeader",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickDateOfReturn",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "PickUpDate",
                table: "OrderHeader");
        }
    }
}
