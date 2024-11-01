using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Healper
{
	public class EmailSetings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient(host: "smtp.gmail.com",port:587) ;
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("sslahelbish@gmail.com",password: "nkvg xces pwlu joxx");
			client.Send("sslahelbish@gmail.com", email.Reseption, email.Subject, email.Bodey);
		}
	}
}
