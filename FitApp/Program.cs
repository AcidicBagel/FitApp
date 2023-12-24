using FitApp.Domain.Models;
using FitApp.View;
using FitApp.Domain.Repository;
using System.Collections.Generic;
using System;
using FitApp.Database;
using System.Linq;

namespace FitApp
{
    class Program
    {
        static void Main(string[] fileNames)
        {
            try
            {
                ClubNet clubNet = GetClubNet(fileNames);
                var consoleFlow = new ConsoleFlow(clubNet);

                consoleFlow.Start();
            }
            catch (Exception e)
            {
                ConsoleUI.PrintError(e);
            }
        }

        private static ClubNet GetClubNet(string[] fileNames)
        {
            var json = new JsonObjectData(fileNames);

            List<FitnessClubDTO> clubsDTO = json.GetObject<FitnessClubDTO>(fileKey: 0);
            List<AccountDTO> accountsDTO = json.GetObject<AccountDTO>(fileKey: 1);

            List<FitnessClub> clubs = clubsDTO.Select(dto => new FitnessClub(dto)).ToList();
            List<Account> accounts = accountsDTO.Select(dto => new Account(dto)).ToList();

            return new ClubNet(clubs, accounts);
        }
    }
}