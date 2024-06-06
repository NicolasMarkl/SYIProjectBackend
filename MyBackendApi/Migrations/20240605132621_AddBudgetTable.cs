using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBackendApi.Migrations
{
    /// <inheritdoc />
    public partial class AddBudgetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AB",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "DB1",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "DB2",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "EV_FV",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "Erfolg_2022",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "GB",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "HH",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "JAHR",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "UG",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "KONTO",
                table: "Budgets",
                newName: "Konto");

            migrationBuilder.RenameColumn(
                name: "TEXT_VASTELLE",
                table: "Budgets",
                newName: "Unterkategorie");

            migrationBuilder.RenameColumn(
                name: "TEXT_KONTO",
                table: "Budgets",
                newName: "Kategorie");

            migrationBuilder.RenameColumn(
                name: "BVA_2024",
                table: "Budgets",
                newName: "Budget2024");

            migrationBuilder.RenameColumn(
                name: "BVA_2023",
                table: "Budgets",
                newName: "Budget2023");

            migrationBuilder.AlterColumn<string>(
                name: "Konto",
                table: "Budgets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsEinnahme",
                table: "Budgets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEinnahme",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "Konto",
                table: "Budgets",
                newName: "KONTO");

            migrationBuilder.RenameColumn(
                name: "Unterkategorie",
                table: "Budgets",
                newName: "TEXT_VASTELLE");

            migrationBuilder.RenameColumn(
                name: "Kategorie",
                table: "Budgets",
                newName: "TEXT_KONTO");

            migrationBuilder.RenameColumn(
                name: "Budget2024",
                table: "Budgets",
                newName: "BVA_2024");

            migrationBuilder.RenameColumn(
                name: "Budget2023",
                table: "Budgets",
                newName: "BVA_2023");

            migrationBuilder.AlterColumn<int>(
                name: "KONTO",
                table: "Budgets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AB",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DB1",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DB2",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EV_FV",
                table: "Budgets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Erfolg_2022",
                table: "Budgets",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GB",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HH",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JAHR",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UG",
                table: "Budgets",
                type: "int",
                nullable: true);
        }
    }
}
