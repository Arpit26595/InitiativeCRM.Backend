using Lead.Domain.Enums;

namespace Lead.Application.DTOs.Response;

public class LeadResponse
{
    public int LeadId { get; set; }
    public DateTime DateOpened { get; set; }
    public LeadStatus Status { get; set; }
    public LeadSource LeadSource { get; set; }
    public LeadType LeadType { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Company { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Description { get; set; }
    public string Details { get; set; }
    public decimal EstimatedValue { get; set; }
    public int Probability { get; set; }
    public DateTime? ExpectedCloseDate { get; set; }
    public int AssignedTo { get; set; }
    public DateTime ConvertedDate { get; set; }
    public int Attachments { get; set; }

    // Computed Properties
    public string FullName { get; set; }
    public string FullAddress { get; set; }
    public bool IsActive { get; set; }
    public int DaysOpen { get; set; }
}