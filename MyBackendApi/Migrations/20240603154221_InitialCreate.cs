using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBackendApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EV_FV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JAHR = table.Column<int>(type: "int", nullable: false),
                    UG = table.Column<int>(type: "int", nullable: false),
                    GB = table.Column<int>(type: "int", nullable: false),
                    DB1 = table.Column<int>(type: "int", nullable: false),
                    DB2 = table.Column<int>(type: "int", nullable: false),
                    HH = table.Column<int>(type: "int", nullable: false),
                    KONTO = table.Column<int>(type: "int", nullable: false),
                    AB = table.Column<int>(type: "int", nullable: false),
                    TEXT_KONTO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TEXT_VASTELLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BVA_2024 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BVA_2023 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Erfolg_2022 = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");
        }
    }
}
