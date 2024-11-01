using System.ComponentModel.DataAnnotations;

namespace Demo.PL.View_Models
{
	public class SinupViewmodel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "First Name Is Required")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name Is Required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Username Is Required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password Is Required")]
		[MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password Is Required")]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password Does Not Match")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "You Must Agree to the Terms and Conditions")]
		[Display(Name = "Agree to Terms and Conditions")]
		public bool IsAgree { get; set; }
	}
}
