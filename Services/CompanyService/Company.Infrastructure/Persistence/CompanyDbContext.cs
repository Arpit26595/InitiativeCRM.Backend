using Company.Domain.Entities;
using Company.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure.Persistence;

public class CompanyDbContext : DbContext
{
    public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options) { }

    public DbSet<Companies> Companies { get; set; }
    public DbSet<CompanyTrades> CompanyTrades { get; set; }
    public DbSet<Contacts> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ──────────────────────────────────────
        // Companies
        // ──────────────────────────────────────
        modelBuilder.Entity<Companies>(entity =>
        {
            entity.HasKey(e => e.CompanyId);
            entity.ToTable("Companies");

            // Enum → string conversions
            entity.Property(e => e.Type)
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasColumnName("Type");

            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasColumnName("Status")
                .HasDefaultValue(CompanyStatus.Active);

            entity.Property(e => e.PaymentTerms)
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasColumnName("PayTerms");

            // Bool → nchar(1) for PreVendor ('Y'/'N')
            entity.Property(e => e.IsPreferredVendor)
                .HasColumnName("PreVendor")
                .HasMaxLength(1)
                .HasConversion(
                    v => v ? "Y" : "N",
                    v => v == "Y");

            // String properties with column name mapping
            entity.Property(e => e.CompanyName)
                .HasMaxLength(60)
                .HasColumnName("CompanyName");

            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasColumnName("Address");

            entity.Property(e => e.City)
                .HasMaxLength(30)
                .HasColumnName("City");

            entity.Property(e => e.State)
                .HasMaxLength(2)
                .HasColumnName("State");

            entity.Property(e => e.Zip)
                .HasMaxLength(10)
                .HasColumnName("Zip");

            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("Phone");

            entity.Property(e => e.Website)
                .HasMaxLength(100)
                .HasColumnName("Website");

            entity.Property(e => e.BondingCompany)
                .HasMaxLength(60)
                .HasColumnName("BondComp");

            entity.Property(e => e.BondingLimit)
                .HasColumnName("BondLimit")
                .HasColumnType("decimal(24,6)")
                .HasPrecision(24, 6);

            entity.Property(e => e.Notes)
                .HasColumnName("Notes")
                .HasColumnType("nvarchar(max)");

            entity.Property(e => e.Attachments)
                .HasColumnName("Attach");

            // Indexes
            entity.HasIndex(e => e.CompanyName)
                .HasDatabaseName("IX_Companies_CompName");

            entity.HasIndex(e => e.Type)
                .HasDatabaseName("IX_Companies_Type");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_Companies_Status");

            entity.HasIndex(e => e.State)
                .HasDatabaseName("IX_Companies_State");
        });

        // ──────────────────────────────────────
        // CompanyTrades
        // ──────────────────────────────────────
        modelBuilder.Entity<CompanyTrades>(entity =>
        {
            entity.HasKey(e => e.CompanyTradeId);
            entity.ToTable("CompanyTrades");

            entity.Property(e => e.Trade)
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasColumnName("Trade");

            entity.Property(e => e.CustomTrade)
                .HasMaxLength(60)
                .HasColumnName("CustomTrade");

            entity.Property(e => e.CompanyId)
                .HasColumnName("CompanyID");

            // Composite unique index: one company can't have the same trade twice
            entity.HasIndex(e => new { e.CompanyId, e.Trade })
                .IsUnique()
                .HasDatabaseName("IX_CompanyTrades_CompanyID_Trade");

            entity.HasIndex(e => e.CompanyId)
                .HasDatabaseName("IX_CompanyTrades_CompanyID");
        });

        // ──────────────────────────────────────
        // Contacts
        // ──────────────────────────────────────
        modelBuilder.Entity<Contacts>(entity =>
        {
            entity.HasKey(e => e.ContactId);
            entity.ToTable("Contacts");

            entity.Property(e => e.CompanyId)
                .HasColumnName("CompanyID");

            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnName("FirstName");

            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .HasColumnName("LastName");

            entity.Property(e => e.Title)
                .HasMaxLength(60)
                .HasColumnName("Title");

            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .HasColumnName("Email");

            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("Phone");

            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .HasColumnName("Mobile");

            entity.Property(e => e.IsPrimary)
                .HasColumnName("IsPrimary");

            entity.Property(e => e.Notes)
                .HasColumnName("Notes")
                .HasColumnType("nvarchar(max)");

            // Indexes
            entity.HasIndex(e => e.CompanyId)
                .HasDatabaseName("IX_Contacts_CompanyID");

            entity.HasIndex(e => e.Email)
                .HasDatabaseName("IX_Contacts_Email");

            entity.HasIndex(e => new { e.CompanyId, e.IsPrimary })
                .HasDatabaseName("IX_Contacts_CompanyID_IsPrimary");
        });
    }
}