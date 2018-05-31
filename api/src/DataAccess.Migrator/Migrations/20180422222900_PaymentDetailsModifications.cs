using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrator.Migrations
{
    public partial class PaymentDetailsModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "PaymentDetails",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VerfiedPayment",
                table: "PaymentDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "VerfiedPayment",
                table: "PaymentDetails");
        }
    }
}
