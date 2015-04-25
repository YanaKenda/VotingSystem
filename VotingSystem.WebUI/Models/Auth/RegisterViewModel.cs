using DataAnnotationsExtensions;

namespace VotingSystem.WebUI.Models.Auth
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class RegisterViewModel
	{
        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Email]
        public string Email { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Surname")]
		public string Surname { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Day of birth")]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }

		[Required(ErrorMessage = "*")]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}