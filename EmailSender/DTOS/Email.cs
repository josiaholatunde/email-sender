using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EmailSender.DTOS
{
    public class Email
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }

        public string From { get; set; }

        [Required]
        public string To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public IFormFile Attachments { get; set; }
        public DateTime DateSent { get; set; }

        public Email()
        {
            DateSent  = DateTime.Now;
            
        }
    }

}
