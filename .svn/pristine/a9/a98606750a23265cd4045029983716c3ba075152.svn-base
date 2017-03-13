using OpenCBS.Messaging.Email;
using System;
using System.Threading.Tasks;

namespace OpenCBS.Messaging.Interfaces
{    
    /// <summary>
    /// contract for email sender 
    /// </summary>
    public interface IEmailSender
    {
        void SendEmail(SmtpContext smtpContext, EmailMessage message);
		Task SendEmailAsync(SmtpContext smtpContext, EmailMessage message); 
    }
}
