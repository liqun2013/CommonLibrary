using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace CommonLibrary.Email
{
	public sealed class EmailOptions
	{
		public string FromAddress { get; set; }
		public string FromName { get; set; }
		public string SmtpHost { get; set; }

		public List<string> MailTo { get; set; }
		public List<string> CC { get; set; }
		public List<string> BCC { get; set; }
		public string Title { get; set; }
		public string MailBody { get; set; }
		public List<string> Attachments { get; set; }
		public Encoding MailEncoding { get; set; }
		public bool IsBodyHtml { get; set; }
		public MailPriority MPriority { get; set; }

		private EmailOptions()
		{
		}

		public EmailOptions(string fromAddress, string fromName, string smtpHost,
			List<string> mailTo, List<string> cc, List<string> bcc, string title, string body, List<string> attachments, Encoding mailEncoding, bool isBodyHtml, MailPriority priority)
		{
			FromAddress = fromAddress;
			FromName = fromName;
			SmtpHost = smtpHost;
			MailTo = mailTo;
			CC = cc;
			BCC = bcc;
			Title = title;
			MailBody = body;
			Attachments = attachments;
			MailEncoding = mailEncoding;
			IsBodyHtml = isBodyHtml;
			MPriority = priority;
		}
	}
}
