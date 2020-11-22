using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBanking.Repository.Migrations
{
    public partial class annualinterestseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnualInterests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<int>(nullable: false),
                    AnnualInterestRate = table.Column<int>(nullable: false),
                    DepositPeriodInDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualInterests", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AnnualInterests",
                columns: new[] { "Id", "AccountType", "AnnualInterestRate", "DepositPeriodInDays" },
                values: new object[,]
                {
                    { 1, 1, 2, 0 },
                    { 2, 2, 1, 30 },
                    { 3, 2, 4, 180 },
                    { 4, 2, 5, 360 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualInterests");
        }
    }
}
