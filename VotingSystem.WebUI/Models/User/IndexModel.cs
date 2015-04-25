namespace VotingSystem.WebUI.Models.User
{
	using System.Collections.Generic;

	public class IndexModel
	{
		public int ItemsPerPage { get; set; }
		public List<User> Users { get; set; }
	}
}