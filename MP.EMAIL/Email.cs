using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace MP.EMAIL
{
    public class Email
    {
        public Email(string from, string to, string subject, string content)
        {
            From = from;
            To = to;
            Subject = subject;
            Content = content;
        }

        public Email(string from, string to, string subject, string content, List<Attachment> attachments)
        {
            From = from;
            To = to;
            Subject = subject;
            Content = content;
            Attachments = attachments?.ToArray();
        }

        public string From { get; }
        public string To { get; }
        public string Subject { get; }
        public string Content { get; }
        public Attachment[] Attachments { get; }
    }
 
    public class Attachment
    {
        public enum ContentType
        {
            PDF,
            XLS,
            TXT,
            ZIP,
            RTF
        }

        public Attachment(string filename, ContentType type)
        {
            Filename = filename;
            Type = type;

            switch (type)
            {
                case ContentType.PDF:
                    SystemType = MediaTypeNames.Application.Pdf;
                    break;
                case ContentType.TXT:
                    SystemType = MediaTypeNames.Application.Rtf;
                    break;
                case ContentType.XLS:
                    SystemType = MediaTypeNames.Application.Octet;
                    break;
                case ContentType.ZIP:
                    SystemType = MediaTypeNames.Application.Zip;
                    break;
                case ContentType.RTF:
                    SystemType = MediaTypeNames.Application.Rtf;
                    break;
            }
        }

        public string Filename { get; set; }
        public ContentType Type { get; set; }
        public string SystemType { get; }
        
    }
}
