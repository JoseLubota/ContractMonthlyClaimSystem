using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractMonthlyClaimSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "claimTBL",
                columns: table => new
                {
                    CLAIM_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    APPROVER_ID = table.Column<int>(type: "int", nullable: true),
                    LECTURER_ID = table.Column<int>(type: "int", nullable: false),
                    NOTES = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HOURS_WORKED = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HOURLY_RATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOCUMENT_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claimTBL", x => x.CLAIM_ID);
                });

            migrationBuilder.CreateTable(
                name: "cmcs_userTBL",
                columns: table => new
                {
                    USERID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FULL_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ACCOUNT_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cmcs_userTBL", x => x.USERID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claimTBL");

            migrationBuilder.DropTable(
                name: "cmcs_userTBL");
        }
    }
}
