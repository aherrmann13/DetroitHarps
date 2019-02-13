using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DetroitHarps.DataAccess.Migrator.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 450, nullable: false),
                    insert_timestamp = table.Column<DateTimeOffset>(nullable: false),
                    update_timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Body = table.Column<string>(maxLength: 450, nullable: false),
                    insert_timestamp = table.Column<DateTimeOffset>(nullable: false),
                    update_timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    insert_timestamp = table.Column<DateTimeOffset>(nullable: false),
                    update_timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SeasonYear = table.Column<int>(nullable: false),
                    Parent_FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    Parent_LastName = table.Column<string>(maxLength: 100, nullable: false),
                    ContactInformation_Email = table.Column<string>(maxLength: 100, nullable: false),
                    ContactInformation_PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    ContactInformation_Address = table.Column<string>(maxLength: 450, nullable: false),
                    ContactInformation_Address2 = table.Column<string>(maxLength: 450, nullable: true),
                    ContactInformation_City = table.Column<string>(maxLength: 100, nullable: false),
                    ContactInformation_State = table.Column<string>(maxLength: 100, nullable: false),
                    ContactInformation_Zip = table.Column<string>(maxLength: 20, nullable: false),
                    PaymentInformation_PaymentTimestamp = table.Column<DateTimeOffset>(nullable: false),
                    PaymentInformation_PaymentType = table.Column<int>(nullable: false),
                    PaymentInformation_Amount = table.Column<double>(nullable: false),
                    RegistrationTimestamp = table.Column<DateTimeOffset>(nullable: false),
                    insert_timestamp = table.Column<DateTimeOffset>(nullable: false),
                    update_timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DisplayProperties_PhotoGroupId = table.Column<int>(nullable: false),
                    DisplayProperties_Title = table.Column<string>(maxLength: 100, nullable: false),
                    DisplayProperties_SortOrder = table.Column<int>(nullable: false),
                    insert_timestamp = table.Column<DateTimeOffset>(nullable: false),
                    update_timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_PhotoGroup_DisplayProperties_PhotoGroupId",
                        column: x => x.DisplayProperties_PhotoGroupId,
                        principalTable: "PhotoGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationChild",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    ShirtSize = table.Column<string>(maxLength: 20, nullable: true),
                    RegistrationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationChild", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationChild_Registration_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotoData",
                columns: table => new
                {
                    PhotoId = table.Column<int>(nullable: false),
                    MimeType = table.Column<string>(maxLength: 450, nullable: true),
                    Data = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoData", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_PhotoData_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_DisplayProperties_PhotoGroupId",
                table: "Photo",
                column: "DisplayProperties_PhotoGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationChild_RegistrationId",
                table: "RegistrationChild",
                column: "RegistrationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "PhotoData");

            migrationBuilder.DropTable(
                name: "RegistrationChild");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "PhotoGroup");
        }
    }
}
