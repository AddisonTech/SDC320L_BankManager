/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
 *
 * Purpose: Account is the BASE CLASS for every account type the bank offers.
 *          It holds what all accounts share (number, balance, open date) and
 *          defines virtual members the derived classes override.
 *          CheckingAccount, SavingsAccount, and CertificateOfDeposit all
 *          inherit from this class.
 */

using System.Globalization;

namespace BankManager.Models
{
    // INHERITANCE: base class. Derived classes extend this with ": Account".
    public class Account
    {
        protected static readonly CultureInfo Usd = CultureInfo.GetCultureInfo("en-US");

        public string AccountNumber { get; }
        public decimal Balance { get; protected set; }
        public DateTime DateOpened { get; }

        public Account(string accountNumber, decimal openingBalance, DateTime dateOpened)
        {
            AccountNumber = accountNumber;
            Balance = openingBalance;
            DateOpened = dateOpened;
        }

        // Friendly name of the account type. Each derived class overrides it.
        public virtual string AccountType => "General Account";

        // Type-specific detail shown in the last column of the account table.
        // Each derived class overrides this with its own rules.
        public virtual string GetFeatureSummary()
        {
            return "Standard account";
        }

        public string FormattedBalance => Balance.ToString("C", Usd);
    }
}
