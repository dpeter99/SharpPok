using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpPok.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    packageID = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    author = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.packageID);
                });

            migrationBuilder.CreateTable(
                name: "Version",
                columns: table => new
                {
                    VersionID = table.Column<string>(type: "varchar(255)", nullable: false),
                    PackageID = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version", x => x.VersionID);
                    table.ForeignKey(
                        name: "FK_Version_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "packageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Version_PackageID",
                table: "Version",
                column: "PackageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Version");

            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
