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

        public ConsoleFlow(ClubNet clubNet) 
        { 
            _clubNet = clubNet; 
        }

        public void Start()
        {
            GetAuthorizedAccount();
            GetChosenClub();
            GetChosenMemberships();

        }

        private void GetAuthorizedAccount()
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

        private void GetChosenClub()
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

        private void GetChosenMemberships()
        {
            Cart cart = new Cart(_authorizedAccount, _selectedClub);

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
                    cart.AddMembership(_selectedClub.MembershipStorage[membershipID].Membership);
                }
                catch (Exception ex)
                {
                    ConsoleUI.PrintError(ex);
                }

                if (ConsoleUI.RequestQuit() == "q")
                    break;
            }
        }

        private static bool IsNumeric(string userInput) => Regex.IsMatch(userInput, @"^\d+$");
    }
}
