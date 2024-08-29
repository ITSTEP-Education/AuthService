using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthJWTAspNetWeb.Migrations
{
    /// <inheritdoc />
    public partial class InjectedModelCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    vinCode = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true, defaultValue: "none"),
                    model = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    volume = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    price = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.id);
                    table.CheckConstraint("ValidPrice", "price > 1000 AND price <= 10000");
                    table.CheckConstraint("ValidVolume", "volume > 0 AND volume <= 6");
                });

            migrationBuilder.CreateIndex(
                name: "IndexNumber",
                table: "cars",
                column: "number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cars");
        }
    }
}
