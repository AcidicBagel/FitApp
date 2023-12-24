using System.Collections.Generic;
using System.Linq;

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
            return _accounts.FirstOrDefault(account => account.IsExists(login, password));
        }

        public List<string> GetClubNames()
        {
            return _fitnessClubs.ConvertAll(club => club.Name);
        }
    }
}
