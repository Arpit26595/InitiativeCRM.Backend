using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Domain.Enums
{
    public enum LeadStatus
    {
        New,
        Qualified,
        Disqualified,
        Proposal,
        Negotiation,
        Won,
        Lost,
        ContactAttempted,
        ContactMade,
        Nurturing
    }
}
