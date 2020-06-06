using System.Threading.Tasks;
using EmailSender.DTOS;

namespace EmailSender.Repositories
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailDTO emailDTO);
    }
}