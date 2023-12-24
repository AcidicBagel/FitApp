using FitApp.Domain.Models;
using System.Collections.Immutable;

namespace FitApp.Domain.Repository
{
    public class FitnessClubDTO
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ImmutableList<MembershipStorage> MembershipStorage { get; private set; }

        public FitnessClubDTO(
            string name,
            string description,
            ImmutableList<MembershipStorage> membershipStorage)
        {
            Name = name;
            Description = description;
            MembershipStorage = membershipStorage;
        }
    }
}
