using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.DTOs.Response
{
    public class LeadNoteResponse
    {
        public int LeadNoteId { get; set; }
        public int LeadId { get; set; }
        public string NoteText { get; set; } = string.Empty;
        public bool IsPrivate { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
