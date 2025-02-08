using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Icon", "Title", "Type" },
                values: new object[,]
                {
                    { 1, "🍔", "Food", "Expense" },
                    { 2, "💰", "Salary", "Income" },
                    { 3, "🚗", "Transport", "Expense" },
                    { 4, "🛍️", "Shopping", "Expense" },
                    { 5, "📈", "Investment", "Income" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Note" },
                values: new object[,]
                {
                    { 1, 50.75m, 1, new DateTime(2025, 2, 5, 12, 12, 16, 187, DateTimeKind.Local).AddTicks(3973), "Lunch" },
                    { 2, 2000.00m, 2, new DateTime(2025, 1, 28, 12, 12, 16, 187, DateTimeKind.Local).AddTicks(3981), "Monthly salary" },
                    { 3, 15.00m, 3, new DateTime(2025, 2, 6, 12, 12, 16, 187, DateTimeKind.Local).AddTicks(3989), "Taxi ride" },
                    { 4, 100.00m, 4, new DateTime(2025, 2, 2, 12, 12, 16, 187, DateTimeKind.Local).AddTicks(3994), "New shoes" },
                    { 5, 300.00m, 5, new DateTime(2025, 2, 4, 12, 12, 16, 187, DateTimeKind.Local).AddTicks(3998), "Stock purchase" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
