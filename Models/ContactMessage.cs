namespace PaulAlarba.Models
{
    public sealed class ContactMessage
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTimeOffset CreatedAtUtc { get; set; }
    }
}
