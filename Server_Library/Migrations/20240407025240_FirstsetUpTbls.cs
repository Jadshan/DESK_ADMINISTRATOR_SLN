using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server_Library.Migrations
{
    /// <inheritdoc />
    public partial class FirstsetUpTbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsersTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsersTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchesTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchesTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeparttmentTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeparttmentTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralDepTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralDepTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TownsTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownsTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CivilId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralDepId = table.Column<int>(type: "int", nullable: false),
                    DeparttmentId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesTbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeesTbl_BranchesTbl_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BranchesTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesTbl_DeparttmentTbl_DeparttmentId",
                        column: x => x.DeparttmentId,
                        principalTable: "DeparttmentTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesTbl_GeneralDepTbl_GeneralDepId",
                        column: x => x.GeneralDepId,
                        principalTable: "GeneralDepTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesTbl_TownsTbl_TownId",
                        column: x => x.TownId,
                        principalTable: "TownsTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesTbl_BranchId",
                table: "EmployeesTbl",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesTbl_DeparttmentId",
                table: "EmployeesTbl",
                column: "DeparttmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesTbl_GeneralDepId",
                table: "EmployeesTbl",
                column: "GeneralDepId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesTbl_TownId",
                table: "EmployeesTbl",
                column: "TownId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsersTbl");

            migrationBuilder.DropTable(
                name: "EmployeesTbl");

            migrationBuilder.DropTable(
                name: "BranchesTbl");

            migrationBuilder.DropTable(
                name: "DeparttmentTbl");

            migrationBuilder.DropTable(
                name: "GeneralDepTbl");

            migrationBuilder.DropTable(
                name: "TownsTbl");
        }
    }
}
