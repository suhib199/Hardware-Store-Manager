using Aspose.Email;
using Aspose.Email.Clients;
using Aspose.Email.Clients.Smtp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Helper
{
    public class EmailHelper
    {
        public static void SendEmail(string recipientEmail, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.Subject = subject;
            message.Body = body;
            message.To.Add(new MailAddress(recipientEmail));
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.ethereal.email";
            client.Port = 587;
            client.Username = "isabella.bauch@ethereal.email";
            client.Password = "WTcDqzHfwhM4MJqP71";
            client.SecurityOptions = SecurityOptions.SSLExplicit; 

        }
    }
}
