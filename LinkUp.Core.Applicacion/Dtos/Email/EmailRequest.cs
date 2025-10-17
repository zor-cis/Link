namespace LinkUp.Core.Applicacion.Dtos.Email
{
    public class EmailRequest
    {
        public string? To { get; set; }
        public required string Subject { get; set; }
        public required string HtmlBody { get; set; }
        public List<string>? ToRange { get; set; } = [];
    }
}
