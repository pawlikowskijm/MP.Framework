using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace MP.EMAIL
{
    public class EmailSender
    {
        SmtpClient _smtpServer { get; set; }
        ICredentialsByHost _credentials { get; set; }

        public EmailSender(string smtpHost, string username, string password, int port)
        {
            _smtpServer = new SmtpClient(smtpHost);
            _smtpServer.Port = port;
            _credentials = new System.Net.NetworkCredential(username, password);
            _smtpServer.EnableSsl = true;
            _smtpServer.UseDefaultCredentials = false;
        }

        public void SendEmail(string from, string to, string subject, string content)
        {
            MailMessage mail = GetMailMessage(from, to, subject, content);
            _smtpServer.Credentials = _credentials;
            
            _smtpServer.Send(mail);
        }

        public void SendEmailWithAttachment(Email email)
        {
            MailMessage mail = GetMailMessage(email);
            _smtpServer.Credentials = _credentials;

            if (email.Attachments != null)
            {
                foreach (var att in email.Attachments)
                {
                    var mailAttachment = new System.Net.Mail.Attachment(att.Filename, att.SystemType);
                    mail.Attachments.Add(mailAttachment);
                }
            }
            
            _smtpServer.Send(mail);
        }

        public void SendEmailWithAttachment(string from, string to, string subject, string content, params System.Net.Mail.Attachment[] attachments)
        {
            MailMessage mail = GetMailMessage(from, to, subject, content);
            _smtpServer.Credentials = _credentials;

            foreach (var att in attachments)
            {
                mail.Attachments.Add(att);
            }

            _smtpServer.Send(mail);
        }

        private MailMessage GetMailMessage(Email email)
        {
            return new MailMessage(email.From, email.To, email.Subject, email.Content);
        }

        private MailMessage GetMailMessage(string from, string to, string subject, string content)
        {
            return new MailMessage(from, to, subject, content);
        }
    }
}
