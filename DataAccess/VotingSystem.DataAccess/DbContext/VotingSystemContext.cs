namespace VotingSystem.DataAccess.DbContext
{
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using Entities;


	public class VotingSystemContext : DbContext
	{
		public VotingSystemContext()
			: base("VotingSystem")
		{

		}

		public DbSet<User> Users { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<Poll> Polls { get; set; }

		public DbSet<Answer> Answers { get; set; }

		public DbSet<Comment> Comments { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove
				<OneToManyCascadeDeleteConvention>();
			base.OnModelCreating(modelBuilder);
		}
	}
}
