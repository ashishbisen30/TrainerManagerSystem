using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainerManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainerVisaAndCerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlternateEmail",
                table: "Trainers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternateMobileNumber",
                table: "Trainers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "Trainers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInUrl",
                table: "Trainers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Trainers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VisaId",
                table: "Trainers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrainerCertification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    IssuingOrganization = table.Column<string>(type: "TEXT", nullable: true),
                    DateObtained = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CertificateUrl = table.Column<string>(type: "TEXT", nullable: true),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerCertification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerCertification_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VisaDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisaType = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsWorkAuthorized = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_VisaId",
                table: "Trainers",
                column: "VisaId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerCertification_TrainerId",
                table: "TrainerCertification",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_VisaDetails_VisaId",
                table: "Trainers",
                column: "VisaId",
                principalTable: "VisaDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_VisaDetails_VisaId",
                table: "Trainers");

            migrationBuilder.DropTable(
                name: "TrainerCertification");

            migrationBuilder.DropTable(
                name: "VisaDetails");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_VisaId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "AlternateEmail",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "AlternateMobileNumber",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "LinkedInUrl",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "VisaId",
                table: "Trainers");
        }
    }
}
