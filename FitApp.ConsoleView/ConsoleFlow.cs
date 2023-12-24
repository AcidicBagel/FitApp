using FitApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FitApp.View
{
    public class ConsoleFlow
    {
        private readonly ClubNet _clubNet;

        private Account _authorizedAccount;
        private FitnessClub _selectedClub;
        private Cart _cart;
        public ConsoleFlow(ClubNet clubNet) 
        { 
            _clubNet = clubNet; 
        }

        public void Start()
        {
            AuthorizeAccount();
            SelectClub();
            _cart = new Cart(_authorizedAccount, _selectedClub);
            SelectMemberships();

            Pay();
        }

        private void AuthorizeAccount()
        {
            Account authorizedAccount = null;
            while (authorizedAccount is null)
            {
                authorizedAccount = _clubNet.FindAccount(
                    ConsoleUI.RequestLogin(), 
                    ConsoleUI.RequestPassword());
            }
            _authorizedAccount = authorizedAccount;
        }

        private void SelectClub()
        {
            FitnessClub selectedClub = null;
            while (selectedClub is null)
            {
                string inputClubID = ConsoleUI.RequestClub(_clubNet.GetClubNames());
                if (IsNumeric(inputClubID))
                {
                    int clubID = Convert.ToInt32(inputClubID);
                    selectedClub = _clubNet.FitnessClubs.ElementAtOrDefault(clubID);
                }
            }
            _selectedClub = selectedClub;
        }

        private void SelectMemberships()
        {
            while (true)
            {
                ConsoleUI.PrintHeader(_authorizedAccount.UserName, _authorizedAccount.Balance);

                string inputMembershipID = ConsoleUI.RequestMemberships(_selectedClub.MembershipStorage);

                if (!IsNumeric(inputMembershipID))
                    continue;

                int membershipID = Convert.ToInt32(inputMembershipID);

                if (_selectedClub.MembershipStorage.ElementAtOrDefault(membershipID) is null)
                    continue;

                try
                {
                    _cart.AddMembership(_selectedClub.MembershipStorage[membershipID].Membership);
                }
                catch (Exception ex)
                {
                    ConsoleUI.PrintError(ex);
                }
                Console.WriteLine(_cart.TotalPrice);
                if (ConsoleUI.RequestQuit().ToLower() == "y")
                    break;
            }
        }

        private void Pay()
        {
            ConsoleUI.PrintHeader(_authorizedAccount.UserName, _authorizedAccount.Balance);
            string answer = ConsoleUI.PrintCart(_cart);

            if(answer.ToLower() == "y") 
            {
                _cart.Pay();
            }

            ConsoleUI.PrintMessage("Bye!");
        }

        private static bool IsNumeric(string userInput) => Regex.IsMatch(userInput, @"^\d+$");
    }
}
