using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RefactorOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbConcertHall",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amountOfPlaces = table.Column<int>(type: "int", nullable: false),
                    basePricing = table.Column<int>(type: "int", nullable: false),
                    isProjectorAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isWifiAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isSoundAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbConcertHall", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbOrders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    hallId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    startTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    endTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    finalPrice = table.Column<float>(type: "float", nullable: false),
                    isProjectorAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isWifiAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isSoundAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbOrders", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbOrders_dbConcertHall_hallId",
                        column: x => x.hallId,
                        principalTable: "dbConcertHall",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_dbOrders_hallId",
                table: "dbOrders",
                column: "hallId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbOrders");

            migrationBuilder.DropTable(
                name: "dbConcertHall");
        }
    }
}
