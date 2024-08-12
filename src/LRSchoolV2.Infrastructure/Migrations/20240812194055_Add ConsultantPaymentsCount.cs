using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRSchoolV2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConsultantPaymentsCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConsultantIsFullyBilled",
                table: "PersonAnnualServiceVariation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ConsultantPaymentsCount",
                table: "PersonAnnualServiceVariation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsultantIsFullyBilled",
                table: "PersonAnnualServiceVariation");

            migrationBuilder.DropColumn(
                name: "ConsultantPaymentsCount",
                table: "PersonAnnualServiceVariation");
        }
    }
}
