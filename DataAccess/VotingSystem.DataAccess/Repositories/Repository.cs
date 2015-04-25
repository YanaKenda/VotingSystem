namespace VotingSystem.DataAccess.Repositories
{
	using System.Data;
	using System.Data.Entity;
	using System.Linq;
	using DbContext;
	using Interfaces.Repositories;
	using Entities;

	public class Repository<T> : IRepository<T>
		where T : class, IEntity, new()
	{
		protected readonly VotingSystemContext Context;
		protected readonly DbSet<T> DbSet;

		public Repository(VotingSystemContext context)
		{
			Context = context;
			DbSet = context.Set<T>();
		}

		public IQueryable<T> All
		{
			get { return DbSet; }
		}

		public T Find(int id)
		{
			return DbSet.Find(id);
		}

		public void InsertOrUpdate(T obj)
		{
			Context.Entry(obj).State = obj.Id == 0 ?
										EntityState.Added :
										EntityState.Modified;
		}

		public void Delete(int id)
		{
			T obj = DbSet.Find(id);
			DbSet.Remove(obj);
		}

		public void Save()
		{
			Context.SaveChanges();
		}

	};
}
