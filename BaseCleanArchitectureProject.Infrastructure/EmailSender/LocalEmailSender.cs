using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using EnsureThat;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Salftech.SharedKernel.Interfaces;

namespace BaseCleanArchitectureProject.Infrastructure.EmailSender {

	public class LocalEmailSender : IEmailSender {
		private readonly ILogger<LocalEmailSender> _logger;
		
		public LocalEmailSender (ILogger<LocalEmailSender> logger) {
			_logger = logger;
			var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			var dirInfor = new DirectoryInfo($"{path}\\Email");
			if (!dirInfor.Exists) {
				dirInfor.Create();
			}
			_path = dirInfor.FullName;
		}

		public string _path { get; set; }

		public async Task SendEmailAsync (string toEmailAddress, string subject, string body, string ccEmailAddress = null, IEnumerable<Attachment> attachments = null) {
			var emailClient = new SmtpClient("localhost");
			emailClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
			emailClient.PickupDirectoryLocation = _path;
			var message = new MailMessage {

												From = new MailAddress("system@localhost.com"),
												Subject = subject,
												Body = body


										};
			message.To.Add(new MailAddress(toEmailAddress));
			await emailClient.SendMailAsync(message);
			_logger.LogWarning($"Sending email to {toEmailAddress} from \"system@localhost.com\" with subject {subject}.");
		}
	}

}