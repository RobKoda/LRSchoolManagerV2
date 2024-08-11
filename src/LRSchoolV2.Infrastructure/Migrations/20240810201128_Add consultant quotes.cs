using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRSchoolV2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addconsultantquotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "InvoiceDocument",
                table: "Consultant",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "QuoteDocument",
                table: "Consultant",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "ConsultantQuote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsultantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuoteConsultantName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    QuoteConsultantAddress = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    EmailSent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantQuote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultantQuote_Consultant_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsultantQuoteItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsultantQuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Denomination = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantQuoteItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultantQuoteItem_ConsultantQuote_ConsultantQuoteId",
                        column: x => x.ConsultantQuoteId,
                        principalTable: "ConsultantQuote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantQuote_ConsultantId",
                table: "ConsultantQuote",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantQuoteItem_ConsultantQuoteId",
                table: "ConsultantQuoteItem",
                column: "ConsultantQuoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultantQuoteItem");

            migrationBuilder.DropTable(
                name: "ConsultantQuote");

            migrationBuilder.DropColumn(
                name: "InvoiceDocument",
                table: "Consultant");

            migrationBuilder.DropColumn(
                name: "QuoteDocument",
                table: "Consultant");
        }
    }
}
