﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamDay.Migrations
{
    /// <inheritdoc />
    public partial class budgetUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SpendAmount",
                table: "Budgets",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AllocatedAmount",
                table: "Budgets",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SpendAmount",
                table: "Budgets",
                type: "double",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "AllocatedAmount",
                table: "Budgets",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }
    }
}
