namespace VotingSystem.BusinessLogic.Managers
{
    using System;
    using System.Linq;
    using VotingSystem.Entities;
    using VotingSystem.Interfaces.Managers;
    using VotingSystem.Interfaces.Repositories;

    public class VotingManager : IVotingManager
    {
        private IVotingRepository votingRepository;

        public VotingManager(IVotingRepository votingRepository)
        {
            this.votingRepository = votingRepository;
        }

        public IQueryable<Voting> Votings
        {
            get { return votingRepository.Votings; }
        }

        public void CreateVoting(Voting voting)
        {
            votingRepository.Add(voting);
        }

        public void DeleteVoting(Guid id)
        {
            //realise soon 
        }

        public IQueryable<Voting> GetActiveVoting()
        {
            return votingRepository.Votings.Where(v => v.EndDateTime > DateTime.Now);
        }

        public IQueryable<Voting> GetClosedVoting()
        {
            return votingRepository.Votings.Where(v => v.EndDateTime <= DateTime.Now);
        }
    }
}

