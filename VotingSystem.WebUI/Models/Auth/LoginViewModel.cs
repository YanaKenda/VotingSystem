using DataAnnotationsExtensions;

namespace VotingSystem.WebUI.Models.Auth
{
	using System.ComponentModel.DataAnnotations;

	public class LoginViewModel
	{
		[Required(ErrorMessage = "*")]
		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		[Email]
		public string Email { get; set; }

		[Required(ErrorMessage = "*")]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]

		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}
}