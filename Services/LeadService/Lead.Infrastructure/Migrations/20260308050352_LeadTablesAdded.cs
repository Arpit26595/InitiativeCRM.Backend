using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lead.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeadTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Leads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Leads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Leads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Leads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Leads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "LeadActivities",
                columns: table => new
                {
                    LeadActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeadId = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Type of activity: Call, Email, Meeting, Task, etc."),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the activity occurred or is scheduled"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Due date for tasks or follow-ups"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Status: Planned, Completed, Cancelled, etc."),
                    AssignedToUserId = table.Column<int>(type: "int", nullable: true, comment: "User ID assigned to this activity"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Whether the note is deleted or not"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "Last modified date"),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "User ID who update the note"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "User ID who created the note")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadActivities", x => x.LeadActivityId);
                });

            migrationBuilder.CreateTable(
                name: "LeadDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeadId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Physical or cloud storage path"),
                    FileType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "MIME type or file extension"),
                    FileSize = table.Column<long>(type: "bigint", nullable: false, comment: "File size in bytes"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UploadedByUserId = table.Column<int>(type: "int", nullable: false, comment: "User ID who uploaded the document"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Whether the note is deleted or not"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "Last modified date"),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "User ID who update the note"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "User ID who created the note")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadDocuments", x => x.DocumentId);
                });

            migrationBuilder.CreateTable(
                name: "LeadNotes",
                columns: table => new
                {
                    LeadNoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeadId = table.Column<int>(type: "int", nullable: false),
                    NoteText = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Note content"),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Whether the note is private (visible only to creator)"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Whether the note is deleted or not"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "Last modified date"),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "User ID who update the note"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "User ID who created the note")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadNotes", x => x.LeadNoteId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivities_ActivityDate",
                table: "LeadActivities",
                column: "ActivityDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivities_AssignedToUserId",
                table: "LeadActivities",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivities_DueDate",
                table: "LeadActivities",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivities_LeadId",
                table: "LeadActivities",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivities_Status",
                table: "LeadActivities",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_LeadDocuments_FileName",
                table: "LeadDocuments",
                column: "FileName");

            migrationBuilder.CreateIndex(
                name: "IX_LeadDocuments_FileType",
                table: "LeadDocuments",
                column: "FileType");

            migrationBuilder.CreateIndex(
                name: "IX_LeadDocuments_IsDeleted",
                table: "LeadDocuments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_LeadDocuments_LeadId",
                table: "LeadDocuments",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadDocuments_UploadedDate",
                table: "LeadDocuments",
                column: "UploadedDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeadNotes_CreatedDate",
                table: "LeadNotes",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeadNotes_IsPrivate",
                table: "LeadNotes",
                column: "IsPrivate");

            migrationBuilder.CreateIndex(
                name: "IX_LeadNotes_LeadId",
                table: "LeadNotes",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadNotes_UpdatedDate",
                table: "LeadNotes",
                column: "UpdatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadActivities");

            migrationBuilder.DropTable(
                name: "LeadDocuments");

            migrationBuilder.DropTable(
                name: "LeadNotes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Leads");
        }
    }
}
