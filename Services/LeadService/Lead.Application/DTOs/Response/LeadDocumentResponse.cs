using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.DTOs.Response
{
    public class LeadDocumentResponse
    {
        public int DocumentId { get; set; }
        public int LeadId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string Description { get; set; }
        public DateTime UploadedDate { get; set; }
        public int UploadedByUserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public int UpdatedBy { get; set; }
        public int CreatedBy { get; set; }
    }
}
