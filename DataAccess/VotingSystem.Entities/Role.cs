namespace VotingSystem.Entities
{
	using System.Collections.Generic;

	public class Role : IEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<User> Users { get; set; }
	}
}
