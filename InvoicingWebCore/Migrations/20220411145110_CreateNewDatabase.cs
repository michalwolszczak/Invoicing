using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoicingWebCore.Migrations
{
    public partial class CreateNewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompaniesInvoices");

            migrationBuilder.DropTable(
                name: "UsersCompanies");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdAddress",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Pesel",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "IdRole",
                table: "Users",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "IdProduct",
                table: "ProductsInvoices",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "IdInvoice",
                table: "ProductsInvoices",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "IdType",
                table: "Invoices",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "IdType",
                table: "Companies",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdAddress",
                table: "Companies",
                newName: "TypeId");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContractorsInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractorId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorsInvoices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "ContractorsInvoices");

            migrationBuilder.DropIndex(
                name: "IX_Companies_UserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Users",
                newName: "IdRole");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductsInvoices",
                newName: "IdProduct");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "ProductsInvoices",
                newName: "IdInvoice");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Invoices",
                newName: "IdType");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Companies",
                newName: "IdType");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Companies",
                newName: "IdAddress");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdAddress",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Pesel",
                table: "Users",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CompaniesInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompany = table.Column<int>(type: "int", nullable: false),
                    IdInvoices = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesInvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompany = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersCompanies", x => x.Id);
                });
        }
    }
}
