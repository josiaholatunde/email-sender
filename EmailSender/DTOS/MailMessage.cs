using System.Collections.Generic;

namespace EmailSender.DTOS
{
    public class MailMessage
    {
        public List<Personalization> Personalizations { get; set; }

        public NameEmail From { get; set; }
        public List<object> Content { get; set; }
        public List<object> Attachments { get; set; }
    }
}