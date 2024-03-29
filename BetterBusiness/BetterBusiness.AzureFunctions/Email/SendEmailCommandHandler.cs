﻿using BetterBusiness.AzureQueueStorage.Messages;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BetterBusiness.AzureFunctions.Email
{
	public interface ISendEmailCommandHandler
	{
		Task Handle(SendEmailCommand command);
	}
	public class SendEmailCommandHandler : ISendEmailCommandHandler
	{
		private readonly EmailConfig _emailConfig;

		public SendEmailCommandHandler(EmailConfig emailConfig)
		{
			_emailConfig = emailConfig;
		}

		public async Task Handle(SendEmailCommand command)
		{
			using (var client = new SmtpClient(_emailConfig.Host, _emailConfig.Port)
			{
				Credentials = new NetworkCredential(_emailConfig.Sender, _emailConfig.Password),
				EnableSsl = true
			})
			using (var message = new MailMessage(_emailConfig.Sender, command.To, command.Subject, command.Body))
			{
				await client.SendMailAsync(message);
			}
		}
	}
}
