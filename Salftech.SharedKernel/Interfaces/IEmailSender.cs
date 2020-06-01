using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Salftech.SharedKernel.Interfaces {

	public interface IEmailSender {
		Task SendEmailAsync (string toEmailAddress, string subject, string body, string ccEmailAddress = null, IEnumerable<Attachment> attachments = null);
	}

}