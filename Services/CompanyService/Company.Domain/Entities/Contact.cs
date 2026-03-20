using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Domain.Entities;

/// <summary>
/// Contacts belong to a Company (1:many).
/// Referenced by Project.Contact field (cross-service, ID only).
/// </summary>
[Table("Contacts")]
public class Contacts
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ContactID")]
    public int ContactId { get; set; }

    /// <summary>
    /// FK to Companies table.
    /// </summary>
    [Required]
    [Column("CompanyID")]
    public int CompanyId { get; set; }

    /// <summary>
    /// Contact first name.
    /// </summary>
    [Required]
    [Column("FirstName")]
    [StringLength(30)]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Contact last name.
    /// </summary>
    [Required]
    [Column("LastName")]
    [StringLength(30)]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Job title or role at the company.
    /// </summary>
    [Column("Title")]
    [StringLength(60)]
    public string? Title { get; set; }

    /// <summary>
    /// Contact email address.
    /// </summary>
    [Column("Email")]
    [StringLength(250)]
    [EmailAddress]
    public string? Email { get; set; }

    /// <summary>
    /// Contact phone number.
    /// </summary>
    [Column("Phone")]
    [StringLength(20)]
    [Phone]
    public string? Phone { get; set; }

    /// <summary>
    /// Mobile phone number.
    /// </summary>
    [Column("Mobile")]
    [StringLength(20)]
    [Phone]
    public string? Mobile { get; set; }

    /// <summary>
    /// Whether this is the primary contact for the company.
    /// </summary>
    [Column("IsPrimary")]
    public bool IsPrimary { get; set; } = false;

    /// <summary>
    /// Additional notes about the contact.
    /// </summary>
    [Column("Notes")]
    public string? Notes { get; set; }

    // ── Computed Properties ──

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}".Trim();
}