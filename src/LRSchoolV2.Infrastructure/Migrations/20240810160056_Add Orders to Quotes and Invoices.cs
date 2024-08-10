using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRSchoolV2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderstoQuotesandInvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CustomerQuoteItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CustomerInvoiceItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "CustomerQuoteItem");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CustomerInvoiceItem");
        }
    }
}
