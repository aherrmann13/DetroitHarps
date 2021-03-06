﻿// <auto-generated />s
using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DetroitHarps.DataAccess.Migrator.Migrations
{
    public partial class AddingEventAttendenceToChildRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrationChildEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Answer = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    EventSnapshot_StartDate = table.Column<DateTime>(nullable: false),
                    EventSnapshot_EndDate = table.Column<DateTime>(nullable: true),
                    EventSnapshot_Title = table.Column<string>(nullable: true),
                    EventSnapshot_Description = table.Column<string>(nullable: true),
                    EventSnapshot_CanRegister = table.Column<bool>(nullable: false),
                    RegistrationChildId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationChildEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationChildEvent_RegistrationChild_RegistrationChildId",
                        column: x => x.RegistrationChildId,
                        principalTable: "RegistrationChild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationChildEvent_RegistrationChildId",
                table: "RegistrationChildEvent",
                column: "RegistrationChildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationChildEvent");
        }
    }
}
