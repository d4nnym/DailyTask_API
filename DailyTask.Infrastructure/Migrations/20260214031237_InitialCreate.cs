using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DAILYTASK");

            migrationBuilder.CreateTable(
                name: "PROJECTS",
                schema: "DAILYTASK",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    CREATED_AT_UTC = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TASKS",
                schema: "DAILYTASK",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PROJECT_ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TITLE = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: false),
                    NOTES = table.Column<string>(type: "NCLOB", maxLength: 4000, nullable: true),
                    IS_DONE = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    CREATED_AT_UTC = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DUE_AT_UTC = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASKS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TASKS_PROJECTS_PROJECT_ID",
                        column: x => x.PROJECT_ID,
                        principalSchema: "DAILYTASK",
                        principalTable: "PROJECTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TASKS_PROJECT_ID",
                schema: "DAILYTASK",
                table: "TASKS",
                column: "PROJECT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TASKS",
                schema: "DAILYTASK");

            migrationBuilder.DropTable(
                name: "PROJECTS",
                schema: "DAILYTASK");
        }
    }
}
