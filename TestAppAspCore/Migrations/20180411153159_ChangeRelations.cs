using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TestAppAspCore.Migrations
{
    public partial class ChangeRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookOrder_Books_BookId",
                table: "BookOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_BookOrder_Orders_OrderId",
                table: "BookOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMenuElement_MenuElements_MenuElementId",
                table: "UserRoleMenuElement");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMenuElement_AspNetRoles_UserRoleId",
                table: "UserRoleMenuElement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMenuElement",
                table: "UserRoleMenuElement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookOrder",
                table: "BookOrder");

            migrationBuilder.RenameTable(
                name: "UserRoleMenuElement",
                newName: "UserRoleMenuElements");

            migrationBuilder.RenameTable(
                name: "BookOrder",
                newName: "BookOrders");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleMenuElement_MenuElementId",
                table: "UserRoleMenuElements",
                newName: "IX_UserRoleMenuElements_MenuElementId");

            migrationBuilder.RenameIndex(
                name: "IX_BookOrder_OrderId",
                table: "BookOrders",
                newName: "IX_BookOrders_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMenuElements",
                table: "UserRoleMenuElements",
                columns: new[] { "UserRoleId", "MenuElementId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookOrders",
                table: "BookOrders",
                columns: new[] { "BookId", "OrderId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookOrders_Books_BookId",
                table: "BookOrders",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookOrders_Orders_OrderId",
                table: "BookOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMenuElements_MenuElements_MenuElementId",
                table: "UserRoleMenuElements",
                column: "MenuElementId",
                principalTable: "MenuElements",
                principalColumn: "MenuElementId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMenuElements_AspNetRoles_UserRoleId",
                table: "UserRoleMenuElements",
                column: "UserRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookOrders_Books_BookId",
                table: "BookOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_BookOrders_Orders_OrderId",
                table: "BookOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMenuElements_MenuElements_MenuElementId",
                table: "UserRoleMenuElements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMenuElements_AspNetRoles_UserRoleId",
                table: "UserRoleMenuElements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMenuElements",
                table: "UserRoleMenuElements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookOrders",
                table: "BookOrders");

            migrationBuilder.RenameTable(
                name: "UserRoleMenuElements",
                newName: "UserRoleMenuElement");

            migrationBuilder.RenameTable(
                name: "BookOrders",
                newName: "BookOrder");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleMenuElements_MenuElementId",
                table: "UserRoleMenuElement",
                newName: "IX_UserRoleMenuElement_MenuElementId");

            migrationBuilder.RenameIndex(
                name: "IX_BookOrders_OrderId",
                table: "BookOrder",
                newName: "IX_BookOrder_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMenuElement",
                table: "UserRoleMenuElement",
                columns: new[] { "UserRoleId", "MenuElementId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookOrder",
                table: "BookOrder",
                columns: new[] { "BookId", "OrderId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookOrder_Books_BookId",
                table: "BookOrder",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookOrder_Orders_OrderId",
                table: "BookOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMenuElement_MenuElements_MenuElementId",
                table: "UserRoleMenuElement",
                column: "MenuElementId",
                principalTable: "MenuElements",
                principalColumn: "MenuElementId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMenuElement_AspNetRoles_UserRoleId",
                table: "UserRoleMenuElement",
                column: "UserRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
