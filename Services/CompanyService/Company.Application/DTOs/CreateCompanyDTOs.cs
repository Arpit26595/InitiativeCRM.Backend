using Company.Domain.Enums;

namespace Company.Application.DTOs;

public class CreateCompanyDTO
{
    public string CompanyName { get; set; } = string.Empty;
    public CompanyType Type { get; set; } = CompanyType.Customer;
    public CompanyStatus Status { get; set; } = CompanyStatus.Active;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string Zip { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? BondingCompany { get; set; }
    public decimal? BondingLimit { get; set; }
    public PaymentTerms? PaymentTerms { get; set; }
    public bool IsPreferredVendor { get; set; } = false;
    public string? Notes { get; set; }

    /// <summary>
    /// Trades to assign. For custom trades, set Trade = "Custom" and provide CustomTrade.
    /// </summary>
    public List<TradeDTO> Trades { get; set; } = [];
}

public class TradeDTO
{
    public Trade Trade { get; set; }
    public string? CustomTrade { get; set; }
}