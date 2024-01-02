using System.Net.Mail;

namespace Meaplus.Models
{
    public class Message
    {

        public string? functionbox_uuid { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? External_text { get; set; }
        public List<SefosParticipant>? sefos_participants { get; set; }
        public List<ExternalParticipant>? External_participants { get; set; }
        public Settings? settings { get; set; }
    }
}
