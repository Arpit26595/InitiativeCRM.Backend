using System;
using System.Collections.Generic;

namespace Lead.Application.Common
{
    /// <summary>
    /// Standard error response
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; } = string.Empty;
        public string? ErrorCode { get; set; }
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public Dictionary<string, List<string>>? ValidationErrors { get; set; }
        public string? StackTrace { get; set; }
    }
}