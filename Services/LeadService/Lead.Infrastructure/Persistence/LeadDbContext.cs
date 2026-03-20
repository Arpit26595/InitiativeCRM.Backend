using Lead.Domain.Entities;
using Lead.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Lead.Infrastructure.Persistence
{
    public class LeadDbContext : DbContext
    {
        public LeadDbContext(DbContextOptions<LeadDbContext> options)
            : base(options)
        {
        }

        public DbSet<Leads> Leads { get; set; }
        public DbSet<LeadActivity> LeadActivities { get; set; }
        public DbSet<LeadNote> LeadNotes { get; set; }
        public DbSet<LeadDocument> LeadDocuments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Leads>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.LeadId);

                // Table Name
                entity.ToTable("Leads");

                // ⭐ Convert enums to strings for database
                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .HasMaxLength(30)
                    .HasColumnName("Status");

                entity.Property(e => e.LeadSource)
                    .HasConversion<string>()
                    .HasMaxLength(30)
                    .HasColumnName("Source");

                entity.Property(e => e.LeadType)
                    .HasConversion<string>()
                    .HasMaxLength(30)
                    .HasColumnName("Type");

                // String properties
                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .HasColumnName("Email");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("Phone");

                entity.Property(e => e.Company)
                    .HasMaxLength(60)
                    .HasColumnName("Company");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasColumnName("Address");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .HasColumnName("City");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .HasColumnName("State");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .HasColumnName("Zip");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("Descrip");

                // DateTime properties
                entity.Property(e => e.DateOpened)
                    .HasColumnName("Opened")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.ExpectedCloseDate)
                    .HasColumnName("CloseBy");

                entity.Property(e => e.ConvertedDate)
                    .HasColumnName("Converted");

                // Decimal property
                entity.Property(e => e.EstimatedValue)
                    .HasColumnName("Estimate")
                    .HasColumnType("decimal(24,8)")
                    .HasPrecision(24, 8);

                // Integer properties
                entity.Property(e => e.Probability)
                    .HasColumnName("Probability");

                entity.Property(e => e.AssignedTo)
                    .HasColumnName("Assigned");

                entity.Property(e => e.Attachments)
                    .HasColumnName("Attachs");

                // Text field
                entity.Property(e => e.Details)
                    .HasColumnName("Detail")
                    .HasColumnType("nvarchar(max)");

                // ⭐ Indexes
                entity.HasIndex(e => e.Email)
                    .HasDatabaseName("IX_Leads_Email");

                entity.HasIndex(e => e.Status)
                    .HasDatabaseName("IX_Leads_Status");

                entity.HasIndex(e => e.AssignedTo)
                    .HasDatabaseName("IX_Leads_AssignedTo");

                entity.HasIndex(e => e.DateOpened)
                    .HasDatabaseName("IX_Leads_DateOpened");

                // ⭐ Default values
                entity.Property(e => e.Status)
                    .HasDefaultValue(LeadStatus.New);
            });

            // =============================================
            // LEAD ACTIVITIES CONFIGURATION
            // =============================================
            modelBuilder.Entity<LeadActivity>(entity =>
            {
                entity.ToTable("LeadActivities");
                entity.HasKey(e => e.LeadActivityId);

                // Properties
                entity.Property(e => e.LeadId)
                    .IsRequired();

                entity.Property(e => e.ActivityType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("Type of activity: Call, Email, Meeting, Task, etc.");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Description)
                    .HasMaxLength(2000);

                entity.Property(e => e.ActivityDate)
                    .IsRequired()
                    .HasComment("Date and time when the activity occurred or is scheduled");

                entity.Property(e => e.DueDate)
                    .HasComment("Due date for tasks or follow-ups");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasComment("Status: Planned, Completed, Cancelled, etc.");

              

            

               

                entity.Property(e => e.AssignedToUserId)
                    .HasComment("User ID assigned to this activity");



                entity.Property(e => e.CreatedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasDefaultValue(0)
                    .HasComment("User ID who created the note");

                entity.Property(e => e.UpdatedBy)
                   .IsRequired()
                   .HasDefaultValue(0)
                   .HasComment("User ID who update the note");

                entity.Property(e => e.UpdatedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()")
                    .HasComment("Last modified date");

                entity.Property(e => e.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false)
                   .HasComment("Whether the note is deleted or not");

                // Indexes
                entity.HasIndex(e => e.LeadId)
                    .HasDatabaseName("IX_LeadActivities_LeadId");

                entity.HasIndex(e => e.ActivityDate)
                    .HasDatabaseName("IX_LeadActivities_ActivityDate");

                entity.HasIndex(e => e.Status)
                    .HasDatabaseName("IX_LeadActivities_Status");

                entity.HasIndex(e => e.AssignedToUserId)
                    .HasDatabaseName("IX_LeadActivities_AssignedToUserId");

                entity.HasIndex(e => e.DueDate)
                    .HasDatabaseName("IX_LeadActivities_DueDate");

            });

            // =============================================
            // LEAD NOTES CONFIGURATION
            // =============================================
            modelBuilder.Entity<LeadNote>(entity =>
            {
                entity.ToTable("LeadNotes");
                entity.HasKey(e => e.LeadNoteId);

                // Properties
                entity.Property(e => e.LeadId)
                    .IsRequired();

                entity.Property(e => e.NoteText)
                    .IsRequired()
                    .HasColumnType("nvarchar(max)")
                    .HasComment("Note content");

                entity.Property(e => e.IsPrivate)
                   .IsRequired()
                   .HasDefaultValue(false)
                   .HasComment("Whether the note is private (visible only to creator)");


                entity.Property(e => e.CreatedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasDefaultValue(0)
                    .HasComment("User ID who created the note");

                entity.Property(e => e.UpdatedBy)
                   .IsRequired()
                   .HasDefaultValue(0)
                   .HasComment("User ID who update the note");

                entity.Property(e => e.UpdatedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()")
                    .HasComment("Last modified date");

                entity.Property(e => e.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false)
                   .HasComment("Whether the note is deleted or not");

               
                

                // Indexes
                entity.HasIndex(e => e.LeadId)
                    .HasDatabaseName("IX_LeadNotes_LeadId");

                entity.HasIndex(e => e.UpdatedDate)
                       .HasDatabaseName("IX_LeadNotes_UpdatedDate");

                entity.HasIndex(e => e.CreatedDate)
                    .HasDatabaseName("IX_LeadNotes_CreatedDate");

                entity.HasIndex(e => e.IsPrivate)
                    .HasDatabaseName("IX_LeadNotes_IsPrivate");
            });

            // =============================================
            // LEAD DOCUMENTS CONFIGURATION
            // =============================================
            modelBuilder.Entity<LeadDocument>(entity =>
            {
                entity.ToTable("LeadDocuments");
                entity.HasKey(e => e.DocumentId);

                // Properties
                entity.Property(e => e.LeadId)
                    .IsRequired();

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("Physical or cloud storage path");

                entity.Property(e => e.FileType)
                    .HasMaxLength(100)
                    .HasComment("MIME type or file extension");

                entity.Property(e => e.FileSize)
                    .IsRequired()
                    .HasComment("File size in bytes");

                entity.Property(e => e.Description)
                    .HasMaxLength(500);

                entity.Property(e => e.UploadedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UploadedByUserId)
                    .IsRequired()
                    .HasComment("User ID who uploaded the document");


                entity.Property(e => e.CreatedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasDefaultValue(0)
                    .HasComment("User ID who created the note");

                entity.Property(e => e.UpdatedBy)
                   .IsRequired()
                   .HasDefaultValue(0)
                   .HasComment("User ID who update the note");

                entity.Property(e => e.UpdatedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()")
                    .HasComment("Last modified date");

                entity.Property(e => e.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false)
                   .HasComment("Whether the note is deleted or not");

                // Indexes
                entity.HasIndex(e => e.LeadId)
                    .HasDatabaseName("IX_LeadDocuments_LeadId");

                entity.HasIndex(e => e.FileName)
                    .HasDatabaseName("IX_LeadDocuments_FileName");

                entity.HasIndex(e => e.UploadedDate)
                    .HasDatabaseName("IX_LeadDocuments_UploadedDate");

                entity.HasIndex(e => e.FileType)
                    .HasDatabaseName("IX_LeadDocuments_FileType");

                entity.HasIndex(e => e.IsDeleted)
                    .HasDatabaseName("IX_LeadDocuments_IsDeleted");

            });

        }
    }
}