using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lead.Domain.Entities;

[Table("LeadDocuments")]
public class LeadDocument
{
    [Key]
    public int DocumentId { get; set; }

    [Required]
    public int LeadId { get; set; }

    [Required]
    [MaxLength(250)]
    public string FileName { get; set; }

    [Required]
    [MaxLength(500)]
    public string FilePath { get; set; }

    [MaxLength(100)]
    public string FileType { get; set; }

    public long FileSize { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public DateTime UploadedDate { get; set; }

    public int UploadedByUserId { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public int UpdatedBy { get;set; }
    public int CreatedBy { get; set; }    

}