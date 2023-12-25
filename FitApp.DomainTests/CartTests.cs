using Xunit;
using System;
using FitApp.Domain.Models;
using FitApp.Domain.Repository;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace FitApp.DomainTests
{
    public class CartTests
    {
        [Fact]
        public void AddMembership_CorrectMembership_AddingMembership()
        {
            var membershipStorages = new List<MembershipStorage>();
            var account = new Account(new AccountDTO("UserName", "Login", "Password", 1, membershipStorages.ToImmutableList()));
            var club = new FitnessClub(new FitnessClubDTO("Name","Description", membershipStorages.ToImmutableList()));
            var cart = new Cart(account, club);
            
            cart.AddMembership(membershipStorages[0].Membership);

            /*Assert.Equal();*/
        }
    }
}
