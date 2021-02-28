using MimeKit;
using System.Threading.Tasks;

namespace EmailAPI.EmailServices
{
    public interface IMessageService
    {
        Task SendEmailAsync(MimeMessage email);
        void EmailBuilderAsync(MessageService requestBody, string subject, string type);
    }
}
