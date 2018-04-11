using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TestAppAspCore.Migrations
{
    public partial class ChangeBookOrderAndOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountOfBook",
                table: "BookOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "BookOrder",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CountOfBook",
                table: "BookOrder");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "BookOrder");
        }
    }
}
