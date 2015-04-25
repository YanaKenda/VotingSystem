namespace VotingSystem.Entities
{
	using System.Collections.Generic;

	public class Answer : IEntity
	{
		public int Id { get; set; }

		public int PollId { get; set; }

		public string AnswerText { get; set; }

		public int Count { get; set; }

		public virtual Poll Poll { get; set; }

		public virtual ICollection<User> Users { get; set; }
	}
}
