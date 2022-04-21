using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoicingWebCore.Migrations
{
    public partial class UpdateInvoiceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tax",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Invoices");
        }
    }
}
