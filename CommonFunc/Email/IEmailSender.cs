using System.Threading.Tasks;

namespace CommonLibrary.Email
{
	public interface IEmailSender
	{
		void Send();
		Task SendAsync();
	}
}
