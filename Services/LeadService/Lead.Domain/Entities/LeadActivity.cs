using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lead.Domain.Entities;

[Table("LeadActivities")]
public class LeadActivity
{
    [Key]
    public int LeadActivityId { get; set; }

    [Required]
    public int LeadId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ActivityType { get; set; } // Call, Email, Meeting, Task

    [Required]
    [MaxLength(200)]
    public string Subject { get; set; }

    [MaxLength(2000)]
    public string Description { get; set; }

    [Required]
    public DateTime ActivityDate { get; set; }

    public DateTime? DueDate { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } // Planned, Completed, Cancelled
    [MaxLength(50)]
    public string Priority { get; set; } // High, Low, Medium, Urgent

    public int? AssignedToUserId { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public int UpdatedBy { get; set; } = 0;
    public int CreatedBy { get; set; } = 0;
}