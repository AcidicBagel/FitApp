﻿using FitApp.Domain.Models;
using System.Collections.Immutable;

namespace FitApp.Domain.Repository
{
    public class AccountDTO
    {
        public string UserName { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public uint Balance { get; private set; }
        public ImmutableList<MembershipStorage> PurchasedMemberships { get; private set; }

        public AccountDTO(
            string username,
            string login, 
            string password, 
            uint balance,
            ImmutableList<MembershipStorage> purchasedMemberships)
        {
            UserName = username;
            Login = login;
            Password = password;
            Balance = balance;
            PurchasedMemberships = purchasedMemberships;
        }
    }
}
