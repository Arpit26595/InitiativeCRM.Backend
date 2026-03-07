using Lead.Domain.Enums;

namespace Lead.Application.DTOs;

public class CreateLeadDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Company { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Address { get; set; }
    public string ZipCode { get; set; }
    public string Details { get; set; }
    public DateTime? ExpectedCloseDate { get; set; }
    public LeadStatus Status { get; set; }
    public LeadSource LeadSource { get; set; }
    public LeadType LeadType { get; set; }
    public string Phone { get; set; }
    public double EstimatedValue { get; set; }
    public int Probability { get; set; }

}
