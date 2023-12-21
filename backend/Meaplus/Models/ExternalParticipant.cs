namespace Meaplus.Models
{
    public class ExternalParticipant
    {
        public string? Email { get; set; }
        public string? Language { get; set; }
        public string? AuthenticationMethod { get; set; }
        public string? AuthenticationIdentifier { get; set; }
        public bool? Configured { get; set; }
    }
}