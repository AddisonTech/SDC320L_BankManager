/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/22/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 2
 *
 * Purpose: TableFormatter turns customers and accounts into fixed-width
 *          tables so every list in the application lines up the same way.
 */

using System.Globalization;
using BankManager.Interfaces;
using BankManager.Models;

namespace BankManager.UI
{
    public static class TableFormatter
    {
        private static readonly CultureInfo Usd = CultureInfo.GetCultureInfo("en-US");

        // Columns: Customer | Account # | Type | Balance | Opened | Details
        private const string AccountRow = "  {0,-18} {1,-11} {2,-9} {3,13}  {4,-10}  {5}";

        // All accounts for all customers, grouped by customer
        public static void PrintAllAccounts(Bank bank)
        {
            PrintAccountHeader();
            decimal grandTotal = 0m;
            int count = 0;

            foreach (Customer customer in bank.Customers)
            {
                foreach (Account account in customer.Accounts)
                {
                    PrintAccountRow(customer.FullName, account);
                    grandTotal += account.Balance;
                    count++;
                }
            }

            ConsoleUI.Line('-');
            Console.WriteLine($"  {count} accounts across {bank.Customers.Count} customers. Total on deposit: {grandTotal.ToString("C", Usd)}");
        }

        // All accounts for one customer, with their contact details on top
        public static void PrintCustomerAccounts(Customer customer)
        {
            PrintCustomerCard(customer);
            PrintAccountHeader();
            foreach (Account account in customer.Accounts)
            {
                PrintAccountRow(customer.FullName, account);
            }
            ConsoleUI.Line('-');
            Console.WriteLine($"  {customer.Accounts.Count} accounts. Combined balance: {customer.TotalBalance().ToString("C", Usd)}");
        }

        public static void PrintCustomerCard(Customer customer)
        {
            Console.WriteLine($"  Customer:  #{customer.CustomerId}  {customer.FullName}");
            Console.WriteLine($"  Address:   {customer.MailingAddress}");
            Console.WriteLine($"  Contact:   {customer.Phone}  |  {customer.Email}");
            Console.WriteLine();
        }

        // Numbered list of one customer's accounts, used to pick an account
        public static void PrintAccountPicker(Customer customer)
        {
            string row = "  {0,-3} {1,-11} {2,-9} {3,13}";
            Console.WriteLine(string.Format(row, "#", "Account #", "Type", "Balance"));
            ConsoleUI.Line('-');
            for (int i = 0; i < customer.Accounts.Count; i++)
            {
                Account a = customer.Accounts[i];
                Console.WriteLine(string.Format(row, i + 1, a.AccountNumber, a.AccountType, a.FormattedBalance));
            }
            Console.WriteLine();
        }

        // Customer list used for the directory and for picking a customer
        public static void PrintCustomerDirectory(Bank bank)
        {
            string row = "  {0,-4} {1,-20} {2,-42} {3,-14} {4,8}";
            Console.WriteLine(string.Format(row, "ID", "Name", "Address", "Phone", "Accounts"));
            ConsoleUI.Line('-');
            foreach (Customer c in bank.Customers)
            {
                Console.WriteLine(string.Format(row, c.CustomerId, c.FullName, c.MailingAddress, c.Phone, c.Accounts.Count));
            }
        }

        // INTERFACE + POLYMORPHISM: walks every account and works only with
        // the ones that implement IInterestBearing. Checking accounts are
        // skipped because they don't implement it. Each Savings and CD object
        // calculates its own interest through the same interface call.
        public static void PrintInterestPreview(Bank bank)
        {
            string row = "  {0,-18} {1,-11} {2,-9} {3,13}  {4,7}  {5,12}";
            Console.WriteLine(string.Format(row, "Customer", "Account #", "Type", "Balance", "Rate", "Interest"));
            ConsoleUI.Line('-');

            decimal total = 0m;
            int count = 0;
            foreach (Customer customer in bank.Customers)
            {
                foreach (Account account in customer.Accounts)
                {
                    if (account is IInterestBearing earner)
                    {
                        decimal interest = earner.CalculateMonthlyInterest();
                        total += interest;
                        count++;
                        Console.WriteLine(string.Format(row,
                            customer.FullName,
                            account.AccountNumber,
                            account.AccountType,
                            account.FormattedBalance,
                            earner.InterestRate.ToString("P2", Usd),
                            interest.ToString("C", Usd)));
                    }
                }
            }

            ConsoleUI.Line('-');
            Console.WriteLine($"  {count} interest-bearing accounts. Interest due this month: {total.ToString("C", Usd)}");
            Console.WriteLine("  Checking accounts do not earn interest and are not listed.");
        }

        private static void PrintAccountHeader()
        {
            Console.WriteLine(string.Format(AccountRow, "Customer", "Account #", "Type", "Balance", "Opened", "Details"));
            ConsoleUI.Line('-');
        }

        // POLYMORPHISM: works for any Account subtype. AccountType and
        // GetFeatureSummary() come from whichever derived class the object
        // really is, even though the parameter type is the base class.
        private static void PrintAccountRow(string customerName, Account account)
        {
            Console.WriteLine(string.Format(AccountRow,
                customerName,
                account.AccountNumber,
                account.AccountType,
                account.FormattedBalance,
                account.DateOpened.ToString("MM/dd/yyyy"),
                account.GetFeatureSummary()));
        }
    }
}
