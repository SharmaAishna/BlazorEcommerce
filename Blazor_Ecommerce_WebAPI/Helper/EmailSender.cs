using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.ComponentModel.DataAnnotations;

namespace Blazor_Ecommerce_WebAPI.Helper
{
    public class EmailSender : IEmailSender
    {
        public string? SendGridSecret { get; set; }
        public EmailSender(IConfiguration _config)
        {
            SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                //var client = new SendGridClient(SendGridSecret);
                ////TODO-add email address from adi 
                //var from = new EmailAddress("emailaddress@adiaccount.com", "Ecommerce");
                //var to = new EmailAddress(email);
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
                //await client.SendEmailAsync(msg);
                var emailToSend = new MimeMessage();
                emailToSend.From.Add(MailboxAddress.Parse("aishnasharma7@gmail.com"));
                emailToSend.To.Add(MailboxAddress.Parse(email));
                emailToSend.Subject = subject;
                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };
                //send email
                using var emailClient = new SmtpClient();
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("aishnasharma7@gmail.com", "luro deal bmhp edlu");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw  ex;
            }



        }
    }
}
