using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDepartment.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oborudovaniye",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateTimeOfService = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oborudovaniye", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sotrudniki",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phone_number = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfReceipt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sotrudniki", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OborudovaniyeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inventory_Oborudovaniye",
                        column: x => x.OborudovaniyeID,
                        principalTable: "Oborudovaniye",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Call",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeCall = table.Column<DateTime>(type: "datetime", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    SotrudnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Call", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Call_Sotrudniki",
                        column: x => x.SotrudnikID,
                        principalTable: "Sotrudniki",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PreventionEvent",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Goal = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SotrudnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreventionEvent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PreventionEvent_Sotrudniki",
                        column: x => x.SotrudnikID,
                        principalTable: "Sotrudniki",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CallOborudovaniye",
                columns: table => new
                {
                    CallID = table.Column<int>(type: "int", nullable: false),
                    OborudovaniyeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallOborudovaniye", x => new { x.CallID, x.OborudovaniyeID });
                    table.ForeignKey(
                        name: "FK_CallOborudovaniye_Call",
                        column: x => x.CallID,
                        principalTable: "Call",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CallOborudovaniye_Oborudovaniye",
                        column: x => x.OborudovaniyeID,
                        principalTable: "Oborudovaniye",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Call_SotrudnikID",
                table: "Call",
                column: "SotrudnikID");

            migrationBuilder.CreateIndex(
                name: "IX_CallID",
                table: "Call",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_CallOborudovaniye_OborudovaniyeID",
                table: "CallOborudovaniye",
                column: "OborudovaniyeID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_OborudovaniyeID",
                table: "Inventory",
                column: "OborudovaniyeID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryID",
                table: "Inventory",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_OborudovaniyeID",
                table: "Oborudovaniye",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_PreventionEvent_SotrudnikID",
                table: "PreventionEvent",
                column: "SotrudnikID");

            migrationBuilder.CreateIndex(
                name: "IX_PreventionEventID",
                table: "PreventionEvent",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_SotrudnikiID",
                table: "Sotrudniki",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallOborudovaniye");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "PreventionEvent");

            migrationBuilder.DropTable(
                name: "Call");

            migrationBuilder.DropTable(
                name: "Oborudovaniye");

            migrationBuilder.DropTable(
                name: "Sotrudniki");
        }
    }
}
