using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.DTOs.Request
{
    public class LeadDocumentRequest
    {
        public int DocumentId { get; set; }
        public int LeadId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public string Description { get; set; }
        public int UploadedByUserId { get; set; } = 0;
        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public int UpdatedBy { get; set; } = 0;
        public int CreatedBy { get; set; } = 0;
    }
}
