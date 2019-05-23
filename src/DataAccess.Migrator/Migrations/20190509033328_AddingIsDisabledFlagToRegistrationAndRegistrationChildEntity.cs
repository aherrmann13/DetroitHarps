﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

namespace DetroitHarps.DataAccess.Migrator.Migrations
{
    public partial class AddingIsDisabledFlagToRegistrationAndRegistrationChildEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "RegistrationChild",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Registration",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "RegistrationChild");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Registration");
        }
    }
}