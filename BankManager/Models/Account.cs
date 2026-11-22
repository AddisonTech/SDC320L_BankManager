/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/22/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 2
 *
 * Purpose: Account is the BASE CLASS for every account type the bank offers.
 *          It holds what all accounts share (number, balance, open date),
 *          implements the ITransactable interface, and defines virtual
 *          members the derived classes override. CheckingAccount,
 *          SavingsAccount, and CertificateOfDeposit all inherit from it.
 */

using System.Globalization;
using BankManager.Interfaces;

namespace BankManager.Models
{
    // INHERITANCE: base class. Derived classes extend this with ": Account".
    // INTERFACE: Account implements ITransactable, so every account type can
    // be handled as an ITransactable.
    public class Account : ITransactable
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
        public virtual string GetFeatureSummary()
        {
            return "Standard account";
        }

        // INTERFACE: ITransactable.Deposit. Deposits follow the same rule for
        // every account type, so this one is not virtual.
        public TransactionResult Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                return TransactionResult.Fail("Deposit amount must be greater than zero.");
            }

            Balance += amount;
            return TransactionResult.Ok($"Deposited {Money(amount)} into {AccountNumber}. New balance: {FormattedBalance}");
        }

        // INTERFACE: ITransactable.ValidateWithdrawal.
        // POLYMORPHISM: virtual, so each derived class replaces this with its
        // own withdrawal rules. Code that calls ValidateWithdrawal() or
        // Withdraw() on an Account or ITransactable reference runs the rules
        // that belong to the real object type. The base rule: no overdrafts.
        public virtual TransactionResult ValidateWithdrawal(decimal amount)
        {
            if (amount <= 0)
            {
                return TransactionResult.Fail("Withdrawal amount must be greater than zero.");
            }

            if (amount > Balance)
            {
                return TransactionResult.Fail($"Insufficient funds. Available balance: {FormattedBalance}");
            }

            return TransactionResult.Ok(string.Empty);
        }

        // INTERFACE: ITransactable.Withdraw.
        // POLYMORPHISM: ValidateWithdrawal() below is a virtual call, so this
        // one method applies Checking, Savings, or CD rules automatically.
        public virtual TransactionResult Withdraw(decimal amount)
        {
            TransactionResult check = ValidateWithdrawal(amount);
            if (!check.Success)
            {
                return check;
            }

            Balance -= amount + check.FeeCharged;
            string message = $"Withdrew {Money(amount)} from {AccountNumber}. New balance: {FormattedBalance}";
            if (check.FeeCharged > 0)
            {
                message += $" (includes {Money(check.FeeCharged)} {check.FeeName})";
            }
            return TransactionResult.Ok(message, check.FeeCharged, check.FeeName);
        }

        public string FormattedBalance => Money(Balance);

        protected static string Money(decimal value) => value.ToString("C", Usd);
    }
}
