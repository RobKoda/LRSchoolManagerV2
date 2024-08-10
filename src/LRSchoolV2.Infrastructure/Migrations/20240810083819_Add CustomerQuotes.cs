using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRSchoolV2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerQuotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerQuote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuoteCustomerName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    QuoteCustomerAddress = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    EmailSent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQuote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerQuote_Person_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerQuoteItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerQuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Denomination = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQuoteItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerQuoteItem_CustomerQuote_CustomerQuoteId",
                        column: x => x.CustomerQuoteId,
                        principalTable: "CustomerQuote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuote_CustomerId",
                table: "CustomerQuote",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuoteItem_CustomerQuoteId",
                table: "CustomerQuoteItem",
                column: "CustomerQuoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerQuoteItem");

            migrationBuilder.DropTable(
                name: "CustomerQuote");
        }
    }
}
