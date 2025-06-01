using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ajansim.Context.Migrations
{
    /// <inheritdoc />
    public partial class siteinfobrandseklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrandId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SiteInfoId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SiteInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Slogan = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FacebookUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    InstagramUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    YouTubeUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TwitterUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FooterText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteInfos", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medias_BrandId",
                table: "Medias",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_SiteInfoId",
                table: "Medias",
                column: "SiteInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Brands_BrandId",
                table: "Medias",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_SiteInfos_SiteInfoId",
                table: "Medias",
                column: "SiteInfoId",
                principalTable: "SiteInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Brands_BrandId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_SiteInfos_SiteInfoId",
                table: "Medias");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "SiteInfos");

            migrationBuilder.DropIndex(
                name: "IX_Medias_BrandId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_SiteInfoId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "SiteInfoId",
                table: "Medias");
        }
    }
}
