using System.ComponentModel.DataAnnotations;

namespace Demo.PL.View_Models
{
	public class ForgetViewMOdel
	{

		[Required(ErrorMessage = "Email Is Reqired")]
		[EmailAddress(ErrorMessage = "Invalid Emaial")]
		public string Email { get; set; }
	}
}
