using System.Text.Json.Serialization;

namespace Shared.Models.Utilities.Filters
{
    public class JsonFilter<T>
    {
        public List<Filter<T>> userFilters { get; set; }
        public List<Filter<T>> adsFilters { get; set; }
        public List<Filter<T>> bankFilters { get; set; }
        public List<Filter<T>> orderFilters { get; set; }
        public List<Filter<T>> walletFilters { get; set; }
        public List<Filter<T>> transactionFilters { get; set; }
        public List<Filter<T>> userRolesFilters { get; set; }
        public List<Filter<T>> redeemFilters { get; set; }
        public List<Filter<T>> rewardsFilters { get; set; }
        public List<Filter<T>> appealFilters { get; set; }
    }
    public class FiltersList<T>
    {
        public List<Filter<T>> Filters { get; set; }
    }
    public class FilterOption
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public class Filter<T>
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;

        [JsonPropertyName("options")]
        public List<T>? Options { get; set; }

        [JsonPropertyName("operation")]
        [JsonConverter(typeof(JsonStringEnumConverter))] // ✅ This ensures "Contains" maps to Operation.Contains
        public Operation Operation { get; set; }

        [JsonPropertyName("controlType")]
        public string ControlType { get; set; } = string.Empty;

        [JsonPropertyName("logicalOperation")]
        public string LogicalOperation { get; set; } = "AND";
    }

    public enum Operation
    {
        Eq, // Equals
        Neq, // Not Equals
        Gt, // Greater Than
        Lt, // Less Than
        Gte, // Greater Than or Equal To
        Lte, // Less Than or Equal To
        Between,
        Contains, // For strings Add more operations as needed
    }

}
