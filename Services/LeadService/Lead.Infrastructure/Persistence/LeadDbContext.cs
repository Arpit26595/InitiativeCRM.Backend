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
        }
    }
}