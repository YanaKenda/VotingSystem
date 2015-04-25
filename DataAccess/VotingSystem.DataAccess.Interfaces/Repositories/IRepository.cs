namespace VotingSystem.DataAccess.Interfaces.Repositories
{
	using System.Linq;

	public interface IRepository<T>
		where T : class
	{
		IQueryable<T> All { get; }

		T Find(int id);

		void InsertOrUpdate(T obj);

		void Delete(int id);

		void Save();
	};
}