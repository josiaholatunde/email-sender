using System.ComponentModel.DataAnnotations;

namespace EmailSender.DTOS
{
    public class NameEmail
    {
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
    }
}