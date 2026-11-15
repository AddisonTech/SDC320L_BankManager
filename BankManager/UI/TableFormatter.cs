/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
 *
 * Purpose: TableFormatter turns customers and accounts into fixed-width
 *          tables so every list in the application lines up the same way.
 */

using System.Globalization;
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
            Console.WriteLine($"  Customer:  #{customer.CustomerId}  {customer.FullName}");
            Console.WriteLine($"  Address:   {customer.MailingAddress}");
            Console.WriteLine($"  Contact:   {customer.Phone}  |  {customer.Email}");
            Console.WriteLine();

            PrintAccountHeader();
            foreach (Account account in customer.Accounts)
            {
                PrintAccountRow(customer.FullName, account);
            }
            ConsoleUI.Line('-');
            Console.WriteLine($"  {customer.Accounts.Count} accounts. Combined balance: {customer.TotalBalance().ToString("C", Usd)}");
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

        private static void PrintAccountHeader()
        {
            Console.WriteLine(string.Format(AccountRow, "Customer", "Account #", "Type", "Balance", "Opened", "Details"));
            ConsoleUI.Line('-');
        }

        // Works for any Account subtype. AccountType and GetFeatureSummary()
        // come from whichever derived class the object really is.
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
