namespace VotingSystem.WebUI.Models
{
	using System.Collections.Generic;
	using VotingSystem.WebUI.Helpers;

	public class VotingsListViewModel
	{
		public IEnumerable<Entities.Poll> Votings { get; set; }
		public PageInfo PageInfo { get; set; }
	}
}