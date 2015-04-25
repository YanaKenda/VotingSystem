namespace VotingSystem.Interfaces.Managers
{
    using System;
    using System.Linq;
    using VotingSystem.Entities;

    public interface IVotingManager
    {
        IQueryable<Voting> Votings { get; }

        void CreateVoting(Voting voting);
        void DeleteVoting(Guid id);
        IQueryable<Voting> GetActiveVoting();
        IQueryable<Voting> GetClosedVoting();

    }
}