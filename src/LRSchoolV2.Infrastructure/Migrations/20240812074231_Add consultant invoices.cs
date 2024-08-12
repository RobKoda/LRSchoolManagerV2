using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRSchoolV2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addconsultantinvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsultantInvoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsultantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceConsultantName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    InvoiceConsultantAddress = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    EmailSent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultantInvoice_Consultant_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsultantInvoiceItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsultantInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Denomination = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantInvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultantInvoiceItem_ConsultantInvoice_ConsultantInvoiceId",
                        column: x => x.ConsultantInvoiceId,
                        principalTable: "ConsultantInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantInvoice_ConsultantId",
                table: "ConsultantInvoice",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantInvoiceItem_ConsultantInvoiceId",
                table: "ConsultantInvoiceItem",
                column: "ConsultantInvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultantInvoiceItem");

            migrationBuilder.DropTable(
                name: "ConsultantInvoice");
        }
    }
}
