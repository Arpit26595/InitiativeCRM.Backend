using Lead.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.DTOs.Response
{
    public class LeadActivityResponse
    {
        public int LeadActivityId { get; set; }
        public int LeadId { get; set; }
        public LeadActivityType ActivityType { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime ActivityDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } // Planned, Completed, Cancelled
        public string Priority { get; set; } // High, Low, Medium, Urgent
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public int UpdatedBy { get; set; } = 0;
        public int CreatedBy { get; set; } = 0;
    }
}
