using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorInCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Courses");
        }
    }
}
