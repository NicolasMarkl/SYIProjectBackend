using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBackendApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIstEinzahlung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsEinnahme",
                table: "Budgets",
                newName: "IstEinzahlung");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IstEinzahlung",
                table: "Budgets",
                newName: "IsEinnahme");
        }
    }
}
