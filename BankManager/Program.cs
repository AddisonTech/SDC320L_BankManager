/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
 *
 * Purpose: Entry point. Loads the sample bank, shows the header and welcome
 *          screen, then runs the main menu loop until the user quits.
 *          Inheritance lives in Models/Account.cs and its three derived
 *          classes; composition lives in Models/Customer.cs and Models/Bank.cs.
 */

using BankManager.Data;
using BankManager.Models;
using BankManager.UI;

namespace BankManager
{
    public class Program
    {
        public static void Main()
        {
            Bank bank = SampleData.CreateBank();

            ConsoleUI.ClearScreen();
            ConsoleUI.ShowHeader();
            ConsoleUI.ShowWelcome();

            bool running = true;
            while (running)
            {
                ConsoleUI.ShowMainMenu();
                string choice = InputHelper.ReadMenuChoice("Select an option", "1", "2", "3");

                switch (choice)
                {
                    case "1":
                        ShowScreen("All accounts for all customers", () => TableFormatter.PrintAllAccounts(bank));
                        break;
                    case "2":
                        ShowOneCustomer(bank);
                        break;
                    case "3":
                        ShowScreen("Customer directory", () => TableFormatter.PrintCustomerDirectory(bank));
                        break;
                    case "Q":
                        running = false;
                        break;
                }
            }

            ConsoleUI.ShowGoodbye();
        }

        // Shows one view, waits for Enter, then redraws the header for the menu
        private static void ShowScreen(string title, Action body)
        {
            ConsoleUI.ClearScreen();
            ConsoleUI.ShowHeader();
            ConsoleUI.ShowSectionTitle(title);
            body();
            ConsoleUI.Pause();
            ConsoleUI.ClearScreen();
            ConsoleUI.ShowHeader();
        }

        private static void ShowOneCustomer(Bank bank)
        {
            ConsoleUI.ClearScreen();
            ConsoleUI.ShowHeader();
            ConsoleUI.ShowSectionTitle("Accounts for one customer");
            TableFormatter.PrintCustomerDirectory(bank);
            Console.WriteLine();

            while (true)
            {
                int? id = InputHelper.ReadInt("Enter a customer ID");
                if (id == null)
                {
                    break;
                }

                Customer? customer = bank.FindCustomer(id.Value);
                if (customer == null)
                {
                    ConsoleUI.ShowError($"No customer with ID {id}. Pick an ID from the list above.");
                    continue;
                }

                ConsoleUI.ShowSectionTitle($"Accounts for {customer.FullName}");
                TableFormatter.PrintCustomerAccounts(customer);
                ConsoleUI.Pause();
                break;
            }

            ConsoleUI.ClearScreen();
            ConsoleUI.ShowHeader();
        }
    }
}
