using Xunit;
using System;
using FitApp.Domain.Models;
using FitApp.Domain.Repository;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace FitApp.Domain.Tests
{
    public class AccountTests
    {
        [Fact]
        public void IsExists_IncorrectAuthorizeData_ReturnsFalse()
        {
            var membershipStorages = new List<MembershipStorage>();
            var account = new Account(new AccountDTO("UserName", "Login", "Password", 0, membershipStorages.ToImmutableList()));

            bool actual = account.IsExists("Invalid", "Invalid");

            Assert.False(actual);
        }

        [Fact]
        public void IsExists_CorrectAuthorizeData_ReturnsTrue()
        {
            var membershipStorages = new List<MembershipStorage>();
            var account = new Account(new AccountDTO("UserName", "Correct", "Correct", 0, membershipStorages.ToImmutableList()));

            bool actual = account.IsExists("Correct", "Correct");

            Assert.True(actual);
        }

        [Fact]
        public void BuyMembership_OverPrice_ReturnsException()
        {
            var membershipStorages = new List<MembershipStorage>();
            var account = new Account(new AccountDTO("UserName", "Login", "Password", 0, membershipStorages.ToImmutableList()));

            Assert.Throws<ArgumentException>(() => account.BuyMembership(membershipStorages, totalPrice: 1));
        }

        [Fact]
        public void BuyMembership_EnoughMoney_AccountMoneyWithdrawn()
        {
            var membershipStorages = new List<MembershipStorage>();
            var account = new Account(new AccountDTO("UserName", "Login", "Password", 1, membershipStorages.ToImmutableList()));

            account.BuyMembership(membershipStorages, totalPrice: 1);

            Assert.Equal("0", account.Balance.ToString());
        }
    }
}
