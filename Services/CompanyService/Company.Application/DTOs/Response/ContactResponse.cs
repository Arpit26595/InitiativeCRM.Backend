namespace Company.Application.DTOs.Response;

public class ContactResponse
{
    public int ContactId { get; set; }
    public int CompanyId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public bool IsPrimary { get; set; }
    public string? Notes { get; set; }

    // Computed
    public string FullName { get; set; } = string.Empty;
}