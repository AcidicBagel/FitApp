using FitApp.Domain.Models;
using System;
using System.Collections.Generic;

namespace FitApp.View
{
    public static class ConsoleUI
    {
        public static void PrintError(Exception ex)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }

        public static void PrintMessage(string message)
        {
            Console.WriteLine($"INFO: {message}");
            Console.ReadKey();
        }

        public static string RequestLogin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Login: ");
            Console.ResetColor();
            return Console.ReadLine() ?? string.Empty;
        }

        public static string RequestPassword()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Password: ");
            Console.ResetColor();
            return Console.ReadLine() ?? string.Empty;
        }

        public static string RequestClub(List<string> clubNames)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("OUR CLUBS");
            Console.ResetColor();
            PrintAllClubNames(clubNames);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Choose a fitness club: ");
            Console.ResetColor();
            return Console.ReadLine() ?? string.Empty;
        }

        private static void PrintAllClubNames(List<string> clubNames)
        {
            for (int i = 0; i < clubNames.Count; i++)
                Console.WriteLine($"{i} {clubNames[i]}");
            Console.WriteLine();
        }

        public static void PrintHeader(string login, uint balance)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Welcome, {login}. Your balance is: ${balance} {Environment.NewLine}");
            Console.ResetColor();
        }

        private static void PrintAllMemberships(IReadOnlyList<MembershipStorage> memberships)
        {
            for (int i = 0; i < memberships.Count; i++)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.Write($"{i}. {memberships[i].Membership.Name}");
                Console.ResetColor();
                Console.WriteLine(
                    $" ({memberships[i].Quantity} left) {Environment.NewLine}" +
                    $"Price: {memberships[i].Membership.Price} {Environment.NewLine}" +
                    $"Description: {memberships[i].Membership.Description} {Environment.NewLine}");
            }
        }

        public static string RequestMemberships(IReadOnlyList<MembershipStorage> memberships)
        {
            PrintAllMemberships(memberships);
            Console.Write("Choose a membership: ");
            return Console.ReadLine() ?? string.Empty;
        }

        public static string RequestQuit()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Environment.NewLine}Enter \"Y\" to go to cart");
            Console.ResetColor();
            return Console.ReadLine()?.ToLower() ?? string.Empty;
        }

        public static string PrintCart(Cart cart)
        {
            Console.WriteLine($"Cart {Environment.NewLine}");
            for(int i = 0; i < cart.SelectedMemberships.Count; i++) 
            {
                Console.WriteLine(
                    $"{i}. {cart.SelectedMemberships[i].Membership.Name} {cart.SelectedMemberships[i].Quantity} pcs.{Environment.NewLine}" +
                    $"Price: ${cart.SelectedMemberships[i].Membership.Price}{Environment.NewLine}");
            }

            Console.WriteLine(
                $"TOTAL PRICE: ${cart.TotalPrice}" +
                $"{Environment.NewLine}Enter \"Y\" to pay");

            return Console.ReadLine();
        }
    }
}
