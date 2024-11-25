using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVC.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    AvailableQuantityBeforeTransaction = table.Column<int>(type: "int", nullable: false),
                    SoldQuantity = table.Column<int>(type: "int", nullable: false),
                    CashierName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "AvailableQuantityBeforeTransaction", "CashierName", "Price", "ProductId", "ProductName", "SoldQuantity", "TimeStamp" },
                values: new object[,]
                {
                    { 1, 50, "Alice", 1200.0, 1001, "Laptop", 1, new DateTime(2024, 11, 24, 22, 32, 57, 428, DateTimeKind.Local).AddTicks(6426) },
                    { 2, 100, "Bob", 800.0, 1002, "Smartphone", 2, new DateTime(2024, 11, 24, 22, 32, 57, 428, DateTimeKind.Local).AddTicks(6493) },
                    { 3, 200, "Charlie", 200.0, 1003, "Headphones", 5, new DateTime(2024, 11, 24, 22, 32, 57, 428, DateTimeKind.Local).AddTicks(6499) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
