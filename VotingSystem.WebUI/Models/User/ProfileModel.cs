namespace VotingSystem.WebUI.Models.User
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	public class ProfileModel
	{
		public int Id { get; set; }

		[DisplayName("First Name")]
		public String Name { get; set; }

		[DisplayName("Last Name")]
		public String Surname { get; set; }

		[DisplayName("Email")]
		public string Email { get; set; }

		[DisplayName("Account types")]
		public List<String> Roles { get; set; }
	}
}