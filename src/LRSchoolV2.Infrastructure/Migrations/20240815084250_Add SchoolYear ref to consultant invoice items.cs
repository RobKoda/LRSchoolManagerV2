using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRSchoolV2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSchoolYearreftoconsultantinvoiceitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchoolYearId",
                table: "ConsultantInvoiceItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantInvoiceItem_SchoolYearId",
                table: "ConsultantInvoiceItem",
                column: "SchoolYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantInvoiceItem_SchoolYear_SchoolYearId",
                table: "ConsultantInvoiceItem",
                column: "SchoolYearId",
                principalTable: "SchoolYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantInvoiceItem_SchoolYear_SchoolYearId",
                table: "ConsultantInvoiceItem");

            migrationBuilder.DropIndex(
                name: "IX_ConsultantInvoiceItem_SchoolYearId",
                table: "ConsultantInvoiceItem");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "ConsultantInvoiceItem");
        }
    }
}
