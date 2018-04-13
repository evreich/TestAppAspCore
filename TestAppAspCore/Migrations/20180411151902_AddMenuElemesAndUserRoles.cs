using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TestAppAspCore.Migrations
{
    public partial class AddMenuElemesAndUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "MenuElements",
                columns: table => new
                {
                    MenuElementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AspActionName = table.Column<string>(nullable: true),
                    AspAreaName = table.Column<string>(nullable: true),
                    AspControllerName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuElements", x => x.MenuElementId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleMenuElement",
                columns: table => new
                {
                    UserRoleId = table.Column<string>(nullable: false),
                    MenuElementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleMenuElement", x => new { x.UserRoleId, x.MenuElementId });
                    table.ForeignKey(
                        name: "FK_UserRoleMenuElement_MenuElements_MenuElementId",
                        column: x => x.MenuElementId,
                        principalTable: "MenuElements",
                        principalColumn: "MenuElementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleMenuElement_AspNetRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMenuElement_MenuElementId",
                table: "UserRoleMenuElement",
                column: "MenuElementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "UserRoleMenuElement");

            migrationBuilder.DropTable(
                name: "MenuElements");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
