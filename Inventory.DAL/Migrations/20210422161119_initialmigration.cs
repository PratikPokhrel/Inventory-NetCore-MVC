using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.DAL.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role_Claim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(maxLength: 50, nullable: true),
                    ClaimValue = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Claim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Claim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(maxLength: 50, nullable: true),
                    ClaimValue = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Claim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Login",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Login", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "User_Token",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Token", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: true),
                    UpdatedByUserId = table.Column<Guid>(nullable: true),
                    DeletedByUserId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_User_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_Role",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    ApplicationRoleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_Role_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalSchema: "dbo",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Role_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: true),
                    UpdatedByUserId = table.Column<Guid>(nullable: true),
                    DeletedByUserId = table.Column<Guid>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Product_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_User_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreatedByUserId",
                table: "Category",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_DeletedByUserId",
                table: "Category",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UpdatedByUserId",
                table: "Category",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                schema: "dbo",
                table: "Product",
                column: "CategoryId");

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

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_ApplicationRoleId",
                schema: "dbo",
                table: "User_Role",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_UserId",
                schema: "dbo",
                table: "User_Role",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Role_Claim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User_Claim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User_Login",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User_Role",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User_Token",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
