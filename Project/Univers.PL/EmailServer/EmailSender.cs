using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using Univers.Models.Models;

namespace Univers.PL.EmailServer
{
    public class EmailSender
    {
        /// <summary>
        /// Sends a verification code to the specified email address.
        /// </summary>
        /// <param name="emailSendTo">The email address to which the verification code will be sent.</param>
        /// <param name="code">The verification code to be sent.</param>
        /// <param name="firstName">The first name of the recipient.</param>
        /// <param name="lastName">The last name of the recipient.</param>
        public void SendCode(string emailSendTo, string code, string firstName, string lastName)  
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("univers.email.verify@gmail.com"));
            email.To.Add(MailboxAddress.Parse($"{emailSendTo}"));
            email.Subject = "Code Verification";
            email.Body = new TextPart(TextFormat.Html) { Text = $"<html><body><h2>Hi <b><i>{firstName} {lastName}</i></b>,</h2><br><h3>Your verification code for changing your password is:</h3><br><h1><b>{code}</b></h1></body></html>" };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp-relay.sendinblue.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("univers.email.verify@gmail.com", "kd8vcAtSFpjI1KGD");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
