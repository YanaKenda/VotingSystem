namespace VotingSystem.WebUI.Models.User
{
	using System;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;

	public class User
	{
		public int Id { get; set; }

		[DisplayName("First Name")]
		public String Name { get; set; }

		[DisplayName("Last Name")]
		public String Surname { get; set; }

		[Required]
		[DisplayName("Password")]
		public String Password { get; set; }

		[Required]
		[DisplayName("Email")]
		public String Email { get; set; }

		[DisplayName("Role")]
		public String[] Role { get; set; }

	}
}