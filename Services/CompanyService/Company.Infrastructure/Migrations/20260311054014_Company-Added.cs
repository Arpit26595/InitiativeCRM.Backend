using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompanyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Active"),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BondComp = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    BondLimit = table.Column<decimal>(type: "decimal(24,6)", precision: 24, scale: 6, nullable: true),
                    PayTerms = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PreVendor = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attach = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTrades",
                columns: table => new
                {
                    CompanyTradeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    Trade = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CustomTrade = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTrades", x => x.CompanyTradeID);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompName",
                table: "Companies",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_State",
                table: "Companies",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Status",
                table: "Companies",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Type",
                table: "Companies",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTrades_CompanyID",
                table: "CompanyTrades",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTrades_CompanyID_Trade",
                table: "CompanyTrades",
                columns: new[] { "CompanyID", "Trade" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompanyID",
                table: "Contacts",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompanyID_IsPrimary",
                table: "Contacts",
                columns: new[] { "CompanyID", "IsPrimary" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email",
                table: "Contacts",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyTrades");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
