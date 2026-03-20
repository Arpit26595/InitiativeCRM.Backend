using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Company.Domain.Enums;

namespace Company.Domain.Entities;

[Table("Companies")]
public class Companies
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CompanyID")]
    public int CompanyId { get; set; }

    /// <summary>
    /// Company name. Flows from Lead when converted.
    /// </summary>
    [Required]
    [Column("CompanyName")]
    [StringLength(60)]
    public string CompanyName { get; set; } = string.Empty;

    /// <summary>
    /// Company type: Customer, Subcontractor, Vendor.
    /// </summary>
    [Required]
    [Column("Type")]
    [StringLength(30)]
    public CompanyType Type { get; set; } = CompanyType.Customer;

    /// <summary>
    /// Company status: Active, Inactive, Out of Business.
    /// Default = Active.
    /// </summary>
    [Required]
    [Column("Status")]
    [StringLength(30)]
    public CompanyStatus Status { get; set; } = CompanyStatus.Active;

    /// <summary>
    /// Street address.
    /// </summary>
    [Required]
    [Column("Address")]
    [StringLength(60)]
    public string? Address { get; set; }

    /// <summary>
    /// City name.
    /// </summary>
    [Required]
    [Column("City")]
    [StringLength(30)]
    public string? City { get; set; }

    /// <summary>
    /// State abbreviation (2 characters).
    /// </summary>
    [Required]
    [Column("State")]
    [StringLength(2)]
    public string? State { get; set; }

    /// <summary>
    /// Zip code (up to 10 characters).
    /// </summary>
    [Required]
    [Column("Zip")]
    [StringLength(10)]
    public string Zip { get; set; } = string.Empty;

    /// <summary>
    /// Company phone number.
    /// </summary>
    [Required]
    [Column("Phone")]
    [StringLength(20)]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Company website URL.
    /// </summary>
    [Column("Website")]
    [StringLength(100)]
    [Url]
    public string? Website { get; set; }

    /// <summary>
    /// Bonding company name.
    /// </summary>
    [Column("BondComp")]
    [StringLength(60)]
    public string? BondingCompany { get; set; }

    /// <summary>
    /// Bonding limit amount.
    /// </summary>
    [Column("BondLimit")]
    public decimal? BondingLimit { get; set; }

    /// <summary>
    /// Payment terms: Net 30, COD, etc.
    /// Applicable for vendors/subcontractors.
    /// </summary>
    [Column("PayTerms")]
    [StringLength(30)]
    public PaymentTerms? PaymentTerms { get; set; }

    /// <summary>
    /// Whether this is a preferred vendor. Checkbox.
    /// </summary>
    [Column("PreVendor")]
    [StringLength(1)]
    public bool IsPreferredVendor { get; set; } = false;

    /// <summary>
    /// Additional info or history.
    /// </summary>
    [Column("Notes")]
    public string? Notes { get; set; }

    /// <summary>
    /// Number of attachments (contracts, insurance certificates, license copies).
    /// </summary>
    [Column("Attach")]
    public int Attachments { get; set; } = 0;

    // ── Computed Properties ──

    [Column("IsActive")]
    public bool IsActive { get; set; } = true;

    [NotMapped]
    public string FullAddress =>
        $"{Address}, {City}, {State} {Zip}".Trim().TrimEnd(',');
}