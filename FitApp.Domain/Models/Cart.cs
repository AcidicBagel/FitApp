using System;
using System.Collections.Generic;
using System.Linq;

namespace FitApp.Domain.Models
{
    public class Cart
    {
        public Account Account { get; private set; }
        public IReadOnlyList<MembershipStorage> SelectedMemberships => _selectedMemberships.AsReadOnly();
        public uint TotalPrice => GetTotalPrice();

        private readonly List<MembershipStorage> _selectedMemberships = new List<MembershipStorage>();
        private readonly FitnessClub _selectedClub;

        public Cart(Account account, FitnessClub selectedClub)
        {
            Account = account;
            _selectedClub = selectedClub;
        }

        public void AddMembership(Membership selectedMembership)
        {
            if (_selectedClub.FindBy(selectedMembership).Quantity <= 0)
                throw new Exception("The membership is out of stock.");

            var existingMembership = _selectedMemberships.FirstOrDefault(m => m.Membership.Equals(selectedMembership));
            if (existingMembership is not null)
                existingMembership.Quantity += 1;
            else
                _selectedMemberships.Add(new MembershipStorage(selectedMembership, 1));
        }

        public void Pay()
        {
            Account.BuyMembership(_selectedMemberships, TotalPrice);

            foreach (var clubMembership in _selectedClub.MembershipStorage)
            {
                foreach(var selectedMemberships in _selectedMemberships)
                {
                    if (clubMembership.Membership == selectedMemberships.Membership)
                    {
                        clubMembership.Quantity -= selectedMemberships.Quantity;
                    }
                }
            }
        }

        private uint GetTotalPrice()
        {
            uint totalPrice = 0;

            if (_selectedMemberships != null)
            {
                foreach (var membership in _selectedMemberships)
                {
                    totalPrice += membership.Membership.Price * membership.Quantity;
                }
            }

            return totalPrice;
        }
    }
}
