using DataAnnotationsExtensions;

namespace VotingSystem.WebUI.Models.Poll
{
	using System.ComponentModel.DataAnnotations;

	public class PollSecurityViewModel
	{
        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Email]
		public string Email { get; set; }
        [Required]
		public string KeyWord { get; set; }

		public bool IsValidEmail { get; set; }

		public bool IsPrivate { get; set; }

        [Required]
		public string EmailApproved { get; set; }

		public int CurrentPollId { get; set; }

		public int CurrentAnswerId { get; set; }

	}
}