using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace WebsiteUI.FunctionsLibrary.EmailServices
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        private string _host;
        private string _password;
        private bool _enabledSSL;
        private string _username;
        private int _port;


        /*new SmtpEmailSender(
            host: builder.Configuration["EmailSender:Host"],
            port: builder.Configuration.GetValue<int>("EmailSender:Port"),
            enabledSSL: builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
            userName: builder.Configuration["EmailSender:UserName"],
            password: builder.Configuration["EmailSender:Password"]
            )*/
        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
            _host = _config.GetSection("EmailHost").Value;
            _password = _config.GetSection("EmailPassword").Value;
            _enabledSSL = bool.Parse(_config.GetSection("EmailEnableSSL").Value);
            _username = _config.GetSection("EmailUserName").Value;
            _port = int.Parse(_config.GetSection("EmailPort").Value);
        }

        public void SendEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_username));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Connect(_host, _port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_username, _password);
                smtp.Send(email);
                smtp.Disconnect(true);
                Console.WriteLine("mail sent.");
            }
        }

        public Task SendEmailAsync(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("kay.schmidt@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("maeve.hilpert@ethereal.email", "mrHjTeURbzaXGpTqTH");
                smtp.Send(email);
                smtp.Disconnect(true);
                Console.WriteLine("mail sent.");

                return smtp.SendAsync(email);
            }
        }
    }
}
