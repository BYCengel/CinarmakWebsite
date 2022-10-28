namespace WebsiteUI.FunctionsLibrary.EmailServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailDTO request);
        public void SendEmail(EmailDTO request);
    }
}
