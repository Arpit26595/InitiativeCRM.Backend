using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.DTOs.Request
{
    public class LeadNoteRequest
    {
        public int LeadNoteId { get; set; }
        [Required]
        public int LeadId { get; set; }

        [Required]
        public string NoteText { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
