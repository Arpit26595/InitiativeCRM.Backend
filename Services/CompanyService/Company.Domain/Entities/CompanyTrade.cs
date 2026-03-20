using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Company.Domain.Enums;

namespace Company.Domain.Entities;

/// <summary>
/// Bridge table: a company can have multiple trades.
/// If Trade == Custom, the CustomTrade field holds the client-defined trade name.
/// </summary>
[Table("CompanyTrades")]
public class CompanyTrades
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CompanyTradeID")]
    public int CompanyTradeId { get; set; }

    /// <summary>
    /// FK to Companies table.
    /// </summary>
    [Required]
    [Column("CompanyID")]
    public int CompanyId { get; set; }

    /// <summary>
    /// Standard trade from enum. Set to Custom if client-defined.
    /// </summary>
    [Required]
    [Column("Trade")]
    [StringLength(30)]
    public Trade Trade { get; set; }

    /// <summary>
    /// Client-defined custom trade name. Only used when Trade == Custom.
    /// </summary>
    [Column("CustomTrade")]
    [StringLength(60)]
    public string? CustomTrade { get; set; }

    // ── Computed ──

    [NotMapped]
    public string TradeName => Trade == Trade.Custom
        ? CustomTrade ?? "Custom"
        : Trade.ToString();
}