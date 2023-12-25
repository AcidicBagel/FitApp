using FitApp.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitApp.Domain.Models
{
    public class Account
    {
        public string UserName { get; private set; }
        public uint Balance { get; private set; }

        private readonly string _login;
        private readonly string _password;
        private List<MembershipStorage> _purchasedMemberships = new List<MembershipStorage>();

        public Account(AccountDTO accountDTO)
        {
            UserName = accountDTO.UserName;
            Balance = accountDTO.Balance;
            _login = accountDTO.Login;
            _password = accountDTO.Password;
            _purchasedMemberships = accountDTO.MembershipStorages.ToList() ?? new List<MembershipStorage>();
        }

        public bool IsExists(string login, string password)
        {
            return login == _login && password == _password;
        }

        public void BuyMembership(List<MembershipStorage> purchasedMemberships, uint totalPrice)
        {
            if (Balance < totalPrice)
                throw new ArgumentException("Not enough money.");

            Balance -= totalPrice;
            _purchasedMemberships.AddRange(purchasedMemberships);
        }
    }
}