using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CommonLibrary.Email
{
	public class EmailSender : IEmailSender
	{
		public EmailOptions Options { get; set; }
		public EmailSender(EmailOptions emailOptions)
		{
			Options = emailOptions;
		}

		public void Send()
		{
			if (Options != null && Options.MailTo?.Any() == true && !string.IsNullOrEmpty(Options.MailBody))
			{
				MailAddress from = new MailAddress(Options.FromAddress, Options.FromName);
				MailMessage mail = new MailMessage { Subject = Options.Title, From = from, Body = Options.MailBody, BodyEncoding = Options.MailEncoding, IsBodyHtml = Options.IsBodyHtml, Priority = Options.MPriority };
				SmtpClient client = new SmtpClient { Host = Options.SmtpHost, DeliveryMethod = SmtpDeliveryMethod.Network };
				try
				{
					foreach (string itm in Options.MailTo)
						mail.To.Add(itm);
					if (Options.CC?.Any() == true)
						foreach (string itm in Options.CC)
							mail.CC.Add(itm);

					if (Options.BCC?.Any() == true)
						foreach (string itm in Options.BCC)
							mail.Bcc.Add(itm);

					if (Options.Attachments?.Any() == true)
						foreach (string att in Options.Attachments)
							mail.Attachments.Add(new Attachment(att));

					client.Send(mail);
				}
				catch (SmtpFailedRecipientsException exc)
				{ throw exc; }
				catch (SmtpException exc)
				{ throw exc; }
				catch
				{ throw; }
				finally
				{
					mail.Dispose();
					client.Dispose();
				}
			}
		}

		public async Task SendAsync()
		{
			if (Options != null && Options.MailTo?.Any() == true && !string.IsNullOrEmpty(Options.MailBody))
			{
				MailAddress from = new MailAddress(Options.FromAddress, Options.FromName);
				MailMessage mail = new MailMessage { Subject = Options.Title, From = from, Body = Options.MailBody, BodyEncoding = Options.MailEncoding, IsBodyHtml = Options.IsBodyHtml, Priority = Options.MPriority };
				SmtpClient client = new SmtpClient { Host = Options.SmtpHost, DeliveryMethod = SmtpDeliveryMethod.Network };
				try
				{
					foreach (string itm in Options.MailTo)
						mail.To.Add(itm);
					if (Options.CC?.Any() == true)
						foreach (string itm in Options.CC)
							mail.CC.Add(itm);

					if (Options.BCC?.Any() == true)
						foreach (string itm in Options.BCC)
							mail.Bcc.Add(itm);

					if (Options.Attachments?.Any() == true)
						foreach (string att in Options.Attachments)
							mail.Attachments.Add(new Attachment(att));

					await client.SendMailAsync(mail).ConfigureAwait(false);
				}
				catch (SmtpFailedRecipientsException exc)
				{ throw exc; }
				catch (SmtpException exc)
				{ throw exc; }
				catch
				{ throw; }
				finally
				{
					mail.Dispose();
					client.Dispose();
				}
			}
		}
	}
}
