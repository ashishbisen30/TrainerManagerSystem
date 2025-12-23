using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainerManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainerVisaAndCerts1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerCertification_Trainers_TrainerId",
                table: "TrainerCertification");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_VisaDetails_VisaId",
                table: "Trainers");

            migrationBuilder.DropTable(
                name: "VisaDetails");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_VisaId",
                table: "Trainers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerCertification",
                table: "TrainerCertification");

            migrationBuilder.RenameTable(
                name: "TrainerCertification",
                newName: "TrainerCertifications");

            migrationBuilder.RenameColumn(
                name: "VisaId",
                table: "Trainers",
                newName: "Visa_IsWorkAuthorized");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerCertification_TrainerId",
                table: "TrainerCertifications",
                newName: "IX_TrainerCertifications_TrainerId");

            migrationBuilder.AddColumn<string>(
                name: "Visa_Country",
                table: "Trainers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Visa_ExpiryDate",
                table: "Trainers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Visa_Id",
                table: "Trainers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Visa_VisaType",
                table: "Trainers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TrainerId",
                table: "TrainerCertifications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerCertifications",
                table: "TrainerCertifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerCertifications_Trainers_TrainerId",
                table: "TrainerCertifications",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerCertifications_Trainers_TrainerId",
                table: "TrainerCertifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerCertifications",
                table: "TrainerCertifications");

            migrationBuilder.DropColumn(
                name: "Visa_Country",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Visa_ExpiryDate",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Visa_Id",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Visa_VisaType",
                table: "Trainers");

            migrationBuilder.RenameTable(
                name: "TrainerCertifications",
                newName: "TrainerCertification");

            migrationBuilder.RenameColumn(
                name: "Visa_IsWorkAuthorized",
                table: "Trainers",
                newName: "VisaId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerCertifications_TrainerId",
                table: "TrainerCertification",
                newName: "IX_TrainerCertification_TrainerId");

            migrationBuilder.AlterColumn<int>(
                name: "TrainerId",
                table: "TrainerCertification",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerCertification",
                table: "TrainerCertification",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "VisaDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsWorkAuthorized = table.Column<bool>(type: "INTEGER", nullable: false),
                    VisaType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_VisaId",
                table: "Trainers",
                column: "VisaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerCertification_Trainers_TrainerId",
                table: "TrainerCertification",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_VisaDetails_VisaId",
                table: "Trainers",
                column: "VisaId",
                principalTable: "VisaDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
