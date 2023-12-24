using System;
using System.Collections.Generic;
using System.Linq;

namespace FitApp.Domain.Models
{
    public class Cart
    {
        public Account Account { get; private set; }
        public IReadOnlyList<MembershipStorage> SelectedMemberships => _selectedMemberships.AsReadOnly();

        private readonly List<MembershipStorage> _selectedMemberships = new List<MembershipStorage>();
        private readonly FitnessClub _selectedClub;

        public Cart(Account account, FitnessClub selectedClub)
        {
            Account = account;
            _selectedClub = selectedClub;
        }

        public void AddMembership(Membership selectedMembership)
        {
            if (Account.Balance < selectedMembership.Price)
                throw new Exception("Not enough money.");

            if (_selectedClub.FindBy(selectedMembership).Quantity <= 0)
                throw new Exception("The membership is out of stock.");

            var existingMembership = _selectedMemberships.FirstOrDefault(m => m.Membership.Equals(selectedMembership));
            if (existingMembership is not null)
            {
                _selectedMemberships.FirstOrDefault(selectedMembership => selectedMembership == existingMembership).Quantity+=1;
            }
            else
            {
                _selectedMemberships.Add(new MembershipStorage(selectedMembership, 1));
            }
        }
    }
}
