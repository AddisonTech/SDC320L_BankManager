/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/22/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 2
 *
 * Purpose: Entry point. Loads the sample bank, shows the header and welcome
 *          screen, then runs the main menu loop until the user quits.
 *          Inheritance lives in Models/Account.cs and its three derived
 *          classes; composition lives in Models/Customer.cs and Models/Bank.cs;
 *          the interfaces live in Interfaces/. The deposit and withdrawal
 *          screens in this file are where polymorphism is put to work.
 */

using BankManager.Data;
using BankManager.Interfaces;
using BankManager.Models;
using BankManager.UI;

namespace BankManager
{
    public class Program
    {
        public static void Main()
        {
            // US dollar formatting everywhere, whatever the machine's region setting
            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");

            Bank bank = SampleData.CreateBank();

            ConsoleUI.ClearScreen();
            ConsoleUI.ShowHeader();
            ConsoleUI.ShowWelcome();

            bool running = true;
            while (running)
            {
                ConsoleUI.ShowMainMenu();
                string choice = InputHelper.ReadMenuChoice("Select an option", "1", "2", "3", "4", "5", "6");

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
                    case "4":
                        ShowScreen("Make a deposit", () => MakeDeposit(bank));
                        break;
                    case "5":
                        ShowScreen("Make a withdrawal", () => MakeWithdrawal(bank));
                        break;
                    case "6":
                        ShowScreen("Month-end interest preview", () => TableFormatter.PrintInterestPreview(bank));
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
            ShowScreen("Accounts for one customer", () =>
            {
                Customer? customer = PickCustomer(bank);
                if (customer != null)
                {
                    ConsoleUI.ShowSectionTitle($"Accounts for {customer.FullName}");
                    TableFormatter.PrintCustomerAccounts(customer);
                }
            });
        }

        private static void MakeDeposit(Bank bank)
        {
            Account? account = PickCustomerAndAccount(bank);
            if (account == null)
            {
                return;
            }

            decimal? amount = InputHelper.ReadAmount("Deposit amount");
            if (amount == null)
            {
                return;
            }

            if (!InputHelper.ReadYesNo($"Deposit {amount.Value:C} into {account.AccountNumber}?"))
            {
                ConsoleUI.ShowWarning("Deposit cancelled. No changes made.");
                return;
            }

            // INTERFACE: the screen works through ITransactable, not a specific class
            ITransactable target = account;
            ShowResult(target.Deposit(amount.Value));
        }

        private static void MakeWithdrawal(Bank bank)
        {
            Account? account = PickCustomerAndAccount(bank);
            if (account == null)
            {
                return;
            }

            decimal? amount = InputHelper.ReadAmount("Withdrawal amount");
            if (amount == null)
            {
                return;
            }

            // INTERFACE: everything below goes through ITransactable.
            // POLYMORPHISM: ValidateWithdrawal() and Withdraw() run the
            // Checking, Savings, or CD rules depending on which object the
            // user picked. This method never checks the account type.
            ITransactable target = account;

            TransactionResult check = target.ValidateWithdrawal(amount.Value);
            if (!check.Success)
            {
                ShowResult(check);
                return;
            }
            if (check.Message.Length > 0)
            {
                ConsoleUI.ShowWarning(check.Message);
            }

            if (!InputHelper.ReadYesNo($"Withdraw {amount.Value:C} from {target.AccountNumber}?"))
            {
                ConsoleUI.ShowWarning("Withdrawal cancelled. No changes made.");
                return;
            }

            ShowResult(target.Withdraw(amount.Value));
        }

        // Asks for a customer ID until it matches a customer or the user cancels
        private static Customer? PickCustomer(Bank bank)
        {
            TableFormatter.PrintCustomerDirectory(bank);
            Console.WriteLine();

            while (true)
            {
                int? id = InputHelper.ReadInt("Enter a customer ID");
                if (id == null)
                {
                    return null;
                }

                Customer? customer = bank.FindCustomer(id.Value);
                if (customer != null)
                {
                    return customer;
                }

                ConsoleUI.ShowError($"No customer with ID {id}. Pick an ID from the list above.");
            }
        }

        private static Account? PickCustomerAndAccount(Bank bank)
        {
            Customer? customer = PickCustomer(bank);
            if (customer == null)
            {
                return null;
            }

            Console.WriteLine();
            Console.WriteLine($"  Accounts for {customer.FullName}:");
            TableFormatter.PrintAccountPicker(customer);

            int? pick = InputHelper.ReadIntInRange("Pick an account", 1, customer.Accounts.Count);
            return pick == null ? null : customer.Accounts[pick.Value - 1];
        }

        private static void ShowResult(TransactionResult result)
        {
            Console.WriteLine();
            if (result.Success)
            {
                ConsoleUI.ShowSuccess(result.Message);
            }
            else
            {
                ConsoleUI.ShowError(result.Message);
            }
        }
    }
}
