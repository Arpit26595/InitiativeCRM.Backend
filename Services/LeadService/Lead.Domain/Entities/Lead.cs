using Lead.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lead.Domain.Entities;
[Table("Leads")]

public class Leads

{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LeadId { get; set; }

    public DateTime DateOpened { get; set; } = DateTime.UtcNow;

    // ⭐ Store as string in DB, use enum in code
    [StringLength(30)]
    public LeadStatus Status { get; set; } = LeadStatus.New;

    [StringLength(30)]
    public LeadSource LeadSource { get; set; } = LeadSource.Other;

    [StringLength(30)]
    public LeadType LeadType { get; set; } = LeadType.NewBuild;

    [StringLength(30)]
    public string FirstName { get; set; }

    [StringLength(30)]
    public string LastName { get; set; }

    [StringLength(20)]
    [Phone]
    public string Phone { get; set; }

    [StringLength(250)]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(60)]
    public string Company { get; set; }

    [StringLength(60)]
    public string Address { get; set; }

    [StringLength(30)]
    public string City { get; set; }

    [StringLength(2)]
    public string State { get; set; }

    [StringLength(10)]
    public string ZipCode { get; set; }

    [StringLength(100)]
    public string Description { get; set; }

    public string Details { get; set; }

    public decimal EstimatedValue { get; set; }

    [StringLength(10)]
    public int Probability { get; set; } // Store as int (0-100)

    public DateTime? ExpectedCloseDate { get; set; }

    public int AssignedTo { get; set; } = 0;

    public DateTime ConvertedDate { get; set; } = DateTime.UtcNow;

    public int Attachments { get; set; } = 0;

    // Computed Properties
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}".Trim();

    [NotMapped]
    public string FullAddress =>
        $"{Address}, {City}, {State} {ZipCode}".Trim().TrimEnd(',');



    [NotMapped]
    public bool IsActive => Status != LeadStatus.Lost && Status != LeadStatus.Won;

    [NotMapped]
    public int DaysOpen => (DateTime.UtcNow - DateOpened).Days;

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public int UpdatedBy { get; set; }
    public int CreatedBy { get; set; }
}



