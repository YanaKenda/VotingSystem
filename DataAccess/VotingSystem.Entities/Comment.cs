using System;

namespace VotingSystem.Entities
{
	public class Comment : IEntity
	{
		public int Id { get; set; }

		public string Text { get; set; }

		public DateTime CommentTime { get; set; }

		public virtual Poll Poll { get; set; }

		public virtual User User { get; set; }
	}
}
