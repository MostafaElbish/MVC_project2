using System.ComponentModel.DataAnnotations;

namespace Demo.PL.View_Models
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password Is Required")]
		[MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirm Password Is Required")]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm Password Does Not Match")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

	}
}
