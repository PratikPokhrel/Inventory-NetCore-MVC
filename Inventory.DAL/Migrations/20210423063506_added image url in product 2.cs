using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.DAL.Migrations
{
    public partial class addedimageurlinproduct2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_CreatedByUserId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_DeletedByUserId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_UpdatedByUserId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CreatedByUserId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_DeletedByUserId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_UpdatedByUserId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreatedBy",
                schema: "dbo",
                table: "Product",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeletedBy",
                schema: "dbo",
                table: "Product",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UpdatedBy",
                schema: "dbo",
                table: "Product",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_CreatedBy",
                schema: "dbo",
                table: "Product",
                column: "CreatedBy",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_DeletedBy",
                schema: "dbo",
                table: "Product",
                column: "DeletedBy",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_UpdatedBy",
                schema: "dbo",
                table: "Product",
                column: "UpdatedBy",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_CreatedBy",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_DeletedBy",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_UpdatedBy",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CreatedBy",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_DeletedBy",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_UpdatedBy",
                schema: "dbo",
                table: "Product");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                schema: "dbo",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedByUserId",
                schema: "dbo",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserId",
                schema: "dbo",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreatedByUserId",
                schema: "dbo",
                table: "Product",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeletedByUserId",
                schema: "dbo",
                table: "Product",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UpdatedByUserId",
                schema: "dbo",
                table: "Product",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_CreatedByUserId",
                schema: "dbo",
                table: "Product",
                column: "CreatedByUserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_DeletedByUserId",
                schema: "dbo",
                table: "Product",
                column: "DeletedByUserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_UpdatedByUserId",
                schema: "dbo",
                table: "Product",
                column: "UpdatedByUserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
