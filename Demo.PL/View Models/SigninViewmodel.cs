using System.ComponentModel.DataAnnotations;

namespace Demo.PL.View_Models
{
	public class SigninViewmodel
	{
	

		[Required(ErrorMessage = "Email Is Reqired")]
		[EmailAddress(ErrorMessage = "Invalid Emaial")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Last Name Is Reqired")]
		[MinLength(length: 5, ErrorMessage = "Minimam Password Lenth is 5")]
		public string PassWord { get; set; }
        public bool Rememberme { get; set; }

    }
}
