using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Reports.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErpReportDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpReportDefinitions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErpReportDefinitions_Name",
                table: "ErpReportDefinitions",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErpReportDefinitions");
        }
    }
}
