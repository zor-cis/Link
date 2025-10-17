namespace LinkUp.Core.Domain.Settings
{
    public class EmailSetting
    {
        public required string EmailFrom { get; set; }
        public required string SmtpHost { get; set; }
        public required int SmtpPort { get; set; }
        public required string SmtpFrom { get; set; }
        public required string SmtpPass { get; set; }
        public required string DisplayName { get; set; }
    }
}
