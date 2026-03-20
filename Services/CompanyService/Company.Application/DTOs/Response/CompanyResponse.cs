using Company.Domain.Enums;

namespace Company.Application.DTOs.Response;

public class CompanyResponse
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public CompanyType Type { get; set; }
    public CompanyStatus Status { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string Zip { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? BondingCompany { get; set; }
    public decimal? BondingLimit { get; set; }
    public PaymentTerms? PaymentTerms { get; set; }
    public bool IsPreferredVendor { get; set; }
    public string? Notes { get; set; }
    public int Attachments { get; set; }

    // Computed
    public bool IsActive { get; set; }
    public string FullAddress { get; set; } = string.Empty;

    // Related data (from same DB — not cross-service)
    public List<CompanyTradeResponse> Trades { get; set; } = [];
    public List<ContactResponse> Contacts { get; set; } = [];
}

public class CompanyTradeResponse
{
    public int CompanyTradeId { get; set; }
    public Trade Trade { get; set; }
    public string? CustomTrade { get; set; }
    public string TradeName { get; set; } = string.Empty;
}