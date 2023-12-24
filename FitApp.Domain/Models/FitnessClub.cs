using FitApp.Domain.Repository;
using System.Collections.Generic;
using System.Linq;

namespace FitApp.Domain.Models
{
    public class FitnessClub
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public IReadOnlyList<MembershipStorage> MembershipStorage => _membershipStorage.AsReadOnly();

        private readonly List<MembershipStorage> _membershipStorage = new();

        public FitnessClub(FitnessClubDTO clubDTO)
        {
            Name = clubDTO.Name;
            Description = clubDTO.Description;
            _membershipStorage = clubDTO.MembershipStorage.ToList();
        }

        public MembershipStorage FindBy(Membership membership)
        {
            return _membershipStorage.FirstOrDefault(storage => storage.Membership == membership);
        }
    }
}