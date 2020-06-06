using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EmailSender.DTOS
{
    public class EmailDTO
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }

        [Required]
        public string To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public IFormFile Attachments { get; set; }

    }

}
