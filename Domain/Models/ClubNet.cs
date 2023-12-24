using System.Collections.Generic;

namespace FitApp.Domain.Models
{
    public class ClubNet
    {
        public IReadOnlyList<Account> Accounts => _accounts.AsReadOnly();
        public IReadOnlyList<FitnessClub> FitnessClubs => _fitnessClubs.AsReadOnly();

        private readonly List<Account> _accounts;
        private readonly List<FitnessClub> _fitnessClubs;

        public ClubNet(List<FitnessClub> fitnessClubs, List<Account> accounts)
        {
            _fitnessClubs = fitnessClubs;
            _accounts = accounts;
        }

        public Account FindAccount(string login, string password)
        {
            foreach (var account in _accounts)
            {
                if (account.IsExists(login, password))
                {
                    return account;
                }
            }
            return null;
        }

        public List<string> GetClubNames()
        {
            return _fitnessClubs.ConvertAll(club => club.Name);
        }
    }
}
