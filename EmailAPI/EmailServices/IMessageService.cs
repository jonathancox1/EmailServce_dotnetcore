using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace EmailAPI.EmailServices
{
    public interface IMessageService
    {
        Task SendEmailAsync(MimeMessage email, IConfiguration config);
        void EmailBuilderAsync(MessageService requestBody, string subject, string type, IConfiguration config);
    }
}
