namespace VotingSystem.Entities
{
	using System;
	using System.Collections.Generic;

	public class Poll : IEntity
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string Name { get; set; }

		public string Question { get; set; }

		public string KeyWord { get; set; }

		public bool IsBlocked { get; set; }

		public bool IsPrivate { get; set; }

		public bool IsOpenResult { get; set; }

		public DateTime StartDateTime { get; set; }

		public DateTime EndDateTime { get; set; }

		public int Count { get; set; }

		public virtual ICollection<Answer> Answers { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }

		public virtual User User { get; set; }
	}
}
