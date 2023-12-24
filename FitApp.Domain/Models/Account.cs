using FitApp.Domain.Repository;
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
        private readonly List<MembershipStorage> _purchasedMemberships = new();

        public Account(AccountDTO accountDTO)
        {
            UserName = accountDTO.UserName;
            Balance = accountDTO.Balance;
            _login = accountDTO.Login;
            _password = accountDTO.Password;
            _purchasedMemberships = accountDTO.PurchasedMemberships?.ToList();
        }

        public bool IsExists(string login, string password)
        {
            return login == _login && password == _password;
        }
    }
}