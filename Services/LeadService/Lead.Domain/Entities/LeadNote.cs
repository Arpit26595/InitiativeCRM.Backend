using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lead.Domain.Entities;

[Table("LeadNotes")]
public class LeadNote
{
    [Key]
    public int LeadNoteId { get; set; }

    [Required]
    public int LeadId { get; set; }

    [Required]
    public string NoteText { get; set; }

    public bool IsPrivate { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public int UpdatedBy { get; set; }
    public int CreatedBy { get; set; }


}