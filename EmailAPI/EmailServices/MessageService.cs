
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EmailAPI.EmailServices
{
    public class MessageService : IMessageService
    {
        private string _fromDisplayName = "Lazy Bones";
        private string _fromEmailAddress = "lazybonescoffeeco@gmail.com";
        public string toName { get; set; }
        public string toEmail { get; set; }
        public List<string> coffees { get; set; }

        public async Task SendEmailAsync(MimeMessage email)
        {
            using(var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync("emailAddress", "password");
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
        }

        public async void EmailBuilderAsync(MessageService requestBody, string subject, string type)
        {
            var success1 = File.ReadAllText(@"C:\Users\Jonathan Cox\source\repos\EmailAPI\EmailAPI\EmailServices\Templates\success1.txt");
            var success2 = File.ReadAllText(@"C:\Users\Jonathan Cox\source\repos\EmailAPI\EmailAPI\EmailServices\Templates\success2.txt");
            var shipped1 = File.ReadAllText(@"C:\Users\Jonathan Cox\source\repos\EmailAPI\EmailAPI\EmailServices\Templates\shipped1.txt");
            var shipped2 = File.ReadAllText(@"C:\Users\Jonathan Cox\source\repos\EmailAPI\EmailAPI\EmailServices\Templates\shipped2.txt");

            var htmlPartOne = type == "success" ? success1 : shipped1;
            var htmlPartTwo = type == "success" ? success2 : shipped2;


            // build to, from, subject
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_fromDisplayName, _fromEmailAddress));
            email.To.Add(new MailboxAddress(requestBody.toName, requestBody.toEmail));
            email.Subject = subject;


            // generate body
            var mailBody = new StringBuilder();
            mailBody.AppendLine(htmlPartOne);

            foreach (var item in requestBody.coffees)
            {
                mailBody.AppendFormat("<p>{0} </p>", item);
            };

            mailBody.AppendFormat(htmlPartTwo);
            var body = new BodyBuilder
            {
                HtmlBody = mailBody.ToString()
            };
            email.Body = body.ToMessageBody();

            // send
            await SendEmailAsync(email);
        }
    }
}
