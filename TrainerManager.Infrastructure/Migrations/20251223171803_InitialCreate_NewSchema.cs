using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainerManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_NewSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Street = table.Column<string>(type: "TEXT", nullable: false),
                    Address_City = table.Column<string>(type: "TEXT", nullable: false),
                    Address_State = table.Column<string>(type: "TEXT", nullable: false),
                    Address_Zip = table.Column<string>(type: "TEXT", nullable: false),
                    Address_Country = table.Column<string>(type: "TEXT", nullable: false),
                    Costing_HourlyRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Costing_Currency = table.Column<string>(type: "TEXT", nullable: false),
                    AccountDetails_BankName = table.Column<string>(type: "TEXT", nullable: true),
                    AccountDetails_AccountNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Field = table.Column<string>(type: "TEXT", nullable: true),
                    Specialization = table.Column<string>(type: "TEXT", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "INTEGER", nullable: false),
                    LastCompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    ProfileImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    ResumePath = table.Column<string>(type: "TEXT", nullable: true),
                    MobileNumber = table.Column<string>(type: "TEXT", nullable: false),
                    AlternateMobileNumber = table.Column<string>(type: "TEXT", nullable: true),
                    AlternateEmail = table.Column<string>(type: "TEXT", nullable: true),
                    LinkedInUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IdentityNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Visa_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    VisaType = table.Column<string>(type: "TEXT", nullable: true),
                    VisaCountry = table.Column<string>(type: "TEXT", nullable: true),
                    VisaExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    VisaIsWorkAuthorized = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainerCertifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    IssuingOrganization = table.Column<string>(type: "TEXT", nullable: true),
                    DateObtained = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CertificateUrl = table.Column<string>(type: "TEXT", nullable: true),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerCertifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerCertifications_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientCompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    TechnologyTaught = table.Column<string>(type: "TEXT", nullable: true),
                    DurationInDays = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingHistories_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerCertifications_TrainerId",
                table: "TrainerCertifications",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingHistories_TrainerId",
                table: "TrainingHistories",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerCertifications");

            migrationBuilder.DropTable(
                name: "TrainingHistories");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
