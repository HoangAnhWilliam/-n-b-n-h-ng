using System.Net.Mail;
using System.Net;

namespace HotelBooking.Areas.Admin.Repository
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true, //bật bảo mật
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("anhhoangtitan2k4@gmail.com", "hyhwxklubpvvpzdc")
            };

            return client.SendMailAsync(
                new MailMessage(from: "anhhoangtitan2k4@gmail.com",
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}
