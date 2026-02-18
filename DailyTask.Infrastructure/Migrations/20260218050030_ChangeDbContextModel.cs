using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDbContextModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TITLE",
                schema: "DAILYTASK",
                table: "TASKS",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "NAME",
                schema: "DAILYTASK",
                table: "PROJECTS",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(200)",
                oldMaxLength: 200);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TITLE",
                schema: "DAILYTASK",
                table: "TASKS",
                type: "NVARCHAR2(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "NAME",
                schema: "DAILYTASK",
                table: "PROJECTS",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255);
        }
    }
}
