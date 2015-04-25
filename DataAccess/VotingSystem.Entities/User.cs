namespace VotingSystem.Entities
{
	using System;
	using System.Collections.Generic;

	public class User : IEntity
	{
		public int Id { get; set; }

		public string Email { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string Password { get; set; }

		public DateTime? DateOfBirth { get; set; }

		public virtual ICollection<Role> Roles { get; set; }

		public virtual ICollection<Poll> Polls { get; set; }

		public virtual ICollection<Answer> Answers { get; set; }

		public virtual ICollection<Comment> Comments { get; set; } 

		public bool IsBanned { get; set; }
	}
}
