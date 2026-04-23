using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace websitepkhoaloi.Migrations
{
    /// <inheritdoc />
    public partial class up_order_titlemenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "TitleMenus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order",
                table: "TitleMenus");
        }
    }
}
