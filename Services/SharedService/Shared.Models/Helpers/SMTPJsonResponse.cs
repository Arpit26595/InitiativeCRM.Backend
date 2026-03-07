/// <summary>
/// Summary description for Class1
/// </summary>
namespace Shared.Models.Helpers
{
    public class SMTPJsonResponse
    {
        public string AdminUser { get; set; }
        public string SMTPEmail { get; set; }
        public string SMTPPassword { get; set; }
        public string SMTPHost { get; set; }
        public string SMTPPort { get; set; }
        public string SMTPSSL { get; set; }
        public string Body { get; set; }
    }
}
