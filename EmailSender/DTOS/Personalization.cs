using System.Collections.Generic;

namespace EmailSender.DTOS
{
    public class Personalization
    {
        public List<NameEmail> To { get; set; }

        public List<NameEmail> Cc { get; set; }

        public List<NameEmail> Bcc { get; set; }

        public string Subject { get; set; }

        
    }
}