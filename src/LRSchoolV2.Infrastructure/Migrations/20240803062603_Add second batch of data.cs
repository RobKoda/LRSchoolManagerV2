using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRSchoolV2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addsecondbatchofdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    StreetComplement = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    City = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnnualService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consultant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BicCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    QuoteDocument = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    InvoiceDocument = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultant_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactPerson1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContactPerson2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillingToContactPerson1 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Person_ContactPerson1Id",
                        column: x => x.ContactPerson1Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Person_ContactPerson2Id",
                        column: x => x.ContactPerson2Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnualServiceVariation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnualServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    InvoiceName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualServiceVariation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualServiceVariation_AnnualService_AnnualServiceId",
                        column: x => x.AnnualServiceId,
                        principalTable: "AnnualService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnualServiceConsultantWork",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnualServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsultantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommonWorkHours = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CommonWorkHoursComment = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualServiceConsultantWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualServiceConsultantWork_AnnualService_AnnualServiceId",
                        column: x => x.AnnualServiceId,
                        principalTable: "AnnualService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnualServiceConsultantWork_Consultant_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnualServiceConsultantWork_SchoolYear_SchoolYearId",
                        column: x => x.SchoolYearId,
                        principalTable: "SchoolYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageRightsGranted = table.Column<bool>(type: "bit", nullable: false),
                    IsFullyBilled = table.Column<bool>(type: "bit", nullable: false),
                    BilledPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonRegistration_Person_BilledPersonId",
                        column: x => x.BilledPersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonRegistration_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonRegistration_SchoolYear_SchoolYearId",
                        column: x => x.SchoolYearId,
                        principalTable: "SchoolYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnualServiceVariationConsultantWork",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnualServiceVariationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsultantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndividualWorkHours = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IndividualWorkHoursComment = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualServiceVariationConsultantWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualServiceVariationConsultantWork_AnnualServiceVariation_AnnualServiceVariationId",
                        column: x => x.AnnualServiceVariationId,
                        principalTable: "AnnualServiceVariation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnualServiceVariationConsultantWork_Consultant_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnualServiceVariationConsultantWork_SchoolYear_SchoolYearId",
                        column: x => x.SchoolYearId,
                        principalTable: "SchoolYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnualServiceVariationYearlyPrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnualServiceVariationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Margin = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualServiceVariationYearlyPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualServiceVariationYearlyPrice_AnnualServiceVariation_AnnualServiceVariationId",
                        column: x => x.AnnualServiceVariationId,
                        principalTable: "AnnualServiceVariation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnualServiceVariationYearlyPrice_SchoolYear_SchoolYearId",
                        column: x => x.SchoolYearId,
                        principalTable: "SchoolYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonAnnualServiceVariation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnualServiceVariationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentsCount = table.Column<int>(type: "int", nullable: false),
                    IsFullyBilled = table.Column<bool>(type: "bit", nullable: false),
                    BilledPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAnnualServiceVariation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAnnualServiceVariation_AnnualServiceVariation_AnnualServiceVariationId",
                        column: x => x.AnnualServiceVariationId,
                        principalTable: "AnnualServiceVariation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonAnnualServiceVariation_Person_BilledPersonId",
                        column: x => x.BilledPersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonAnnualServiceVariation_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonAnnualServiceVariation_SchoolYear_SchoolYearId",
                        column: x => x.SchoolYearId,
                        principalTable: "SchoolYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnualServiceConsultantWork_AnnualServiceId",
                table: "AnnualServiceConsultantWork",
                column: "AnnualServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualServiceConsultantWork_ConsultantId",
                table: "AnnualServiceConsultantWork",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualServiceConsultantWork_SchoolYearId",
                table: "AnnualServiceConsultantWork",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualServiceVariation_AnnualServiceId",
                table: "AnnualServiceVariation",
                column: "AnnualServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualServiceVariationConsultantWork_AnnualServiceVariationId",
                table: "AnnualServiceVariationConsultantWork",
                column: "AnnualServiceVariationId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualServiceVariationConsultantWork_ConsultantId",
                table: "AnnualServiceVariationConsultantWork",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualServiceVariationConsultantWork_SchoolYearId",
                table: "AnnualServiceVariationConsultantWork",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualServiceVariationYearlyPrice_AnnualServiceVariationId",
                table: "AnnualServiceVariationYearlyPrice",
                column: "AnnualServiceVariationId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualServiceVariationYearlyPrice_SchoolYearId",
                table: "AnnualServiceVariationYearlyPrice",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultant_AddressId",
                table: "Consultant",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_AddressId",
                table: "Person",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_ContactPerson1Id",
                table: "Person",
                column: "ContactPerson1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Person_ContactPerson2Id",
                table: "Person",
                column: "ContactPerson2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAnnualServiceVariation_AnnualServiceVariationId",
                table: "PersonAnnualServiceVariation",
                column: "AnnualServiceVariationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAnnualServiceVariation_BilledPersonId",
                table: "PersonAnnualServiceVariation",
                column: "BilledPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAnnualServiceVariation_PersonId",
                table: "PersonAnnualServiceVariation",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAnnualServiceVariation_SchoolYearId",
                table: "PersonAnnualServiceVariation",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRegistration_BilledPersonId",
                table: "PersonRegistration",
                column: "BilledPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRegistration_PersonId",
                table: "PersonRegistration",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRegistration_SchoolYearId",
                table: "PersonRegistration",
                column: "SchoolYearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualServiceConsultantWork");

            migrationBuilder.DropTable(
                name: "AnnualServiceVariationConsultantWork");

            migrationBuilder.DropTable(
                name: "AnnualServiceVariationYearlyPrice");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "PersonAnnualServiceVariation");

            migrationBuilder.DropTable(
                name: "PersonRegistration");

            migrationBuilder.DropTable(
                name: "Consultant");

            migrationBuilder.DropTable(
                name: "AnnualServiceVariation");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "AnnualService");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
