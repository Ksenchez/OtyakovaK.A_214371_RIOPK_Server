using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace shop_back.Migrations
{
    /// <inheritdoc />
    public partial class InitPA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PricingPolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PolicyName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    DefaultMarkupPercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingPolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    BaseCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    MarkupPercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PricingPolicyId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_PricingPolicies_PricingPolicyId",
                        column: x => x.PricingPolicyId,
                        principalTable: "PricingPolicies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductCalculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CalculatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomMarkup = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCalculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCalculations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCalculations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PricingPolicies",
                columns: new[] { "Id", "DefaultMarkupPercent", "Description", "IsActive", "PolicyName" },
                values: new object[,]
                {
                    { 1, 10m, "Базовая наценка 10%", true, "Стандартная" },
                    { 2, 20m, "Увеличенная наценка 20%", true, "Премиум" },
                    { 3, 5m, "Минимальная наценка 5%", true, "Льготная" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BaseCost", "CreatedAt", "Description", "MarkupPercent", "Name", "PricingPolicyId" },
                values: new object[] { 1, 100m, new DateTime(2025, 4, 15, 17, 2, 28, 107, DateTimeKind.Utc).AddTicks(9319), "Молоко 950мл.", 15m, "Молоко", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "$2a$11$pbB5Z8u9HKk0Ani/IIh60OIr4l3prpI4KgY1.j4XeBAN0mtdag/eO", "admin", "admin" },
                    { 2, "$2a$11$EjJ0CWEUr6X6CmsF8aGBgu4h3tHid1SLHt.5wZJrvbMl9OZtrfTOS", "user", "user" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BaseCost", "CreatedAt", "Description", "MarkupPercent", "Name", "PricingPolicyId" },
                values: new object[,]
                {
                    { 2, 200m, new DateTime(2025, 4, 15, 17, 2, 28, 107, DateTimeKind.Utc).AddTicks(9321), "Масло 200гр.", 10m, "Масло", 1 },
                    { 3, 150m, new DateTime(2025, 4, 15, 17, 2, 28, 107, DateTimeKind.Utc).AddTicks(9323), "Coca-Cola 2л.", 20m, "Coca-Cola", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCalculations_ProductId",
                table: "ProductCalculations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCalculations_UserId",
                table: "ProductCalculations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PricingPolicyId",
                table: "Products",
                column: "PricingPolicyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCalculations");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PricingPolicies");
        }
    }
}
