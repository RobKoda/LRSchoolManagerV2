using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRSchoolV2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Secondbatchoftablesfromv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckDeposit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckDeposit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInvoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceCustomerName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    InvoiceCustomerAddress = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    EmailSent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerInvoice_Person_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerPaymentTypeValue = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPayment_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInvoiceItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Denomination = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerInvoiceItem_CustomerInvoice_CustomerInvoiceId",
                        column: x => x.CustomerInvoiceId,
                        principalTable: "CustomerInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckDepositPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckDepositId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckDepositPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckDepositPayment_CheckDeposit_CheckDepositId",
                        column: x => x.CheckDepositId,
                        principalTable: "CheckDeposit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckDepositPayment_CustomerPayment_CustomerPaymentId",
                        column: x => x.CustomerPaymentId,
                        principalTable: "CustomerPayment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckDepositPayment_CheckDepositId",
                table: "CheckDepositPayment",
                column: "CheckDepositId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckDepositPayment_CustomerPaymentId",
                table: "CheckDepositPayment",
                column: "CustomerPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInvoice_CustomerId",
                table: "CustomerInvoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInvoiceItem_CustomerInvoiceId",
                table: "CustomerInvoiceItem",
                column: "CustomerInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPayment_PersonId",
                table: "CustomerPayment",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckDepositPayment");

            migrationBuilder.DropTable(
                name: "CustomerInvoiceItem");

            migrationBuilder.DropTable(
                name: "CheckDeposit");

            migrationBuilder.DropTable(
                name: "CustomerPayment");

            migrationBuilder.DropTable(
                name: "CustomerInvoice");
        }
    }
}
