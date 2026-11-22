/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/22/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 2
 *
 * Purpose: ITransactable is the contract for anything that can take money in
 *          and pay money out. The menu screens for deposits and withdrawals
 *          only talk to this interface, so they work the same for every
 *          account type without knowing which type they have.
 */

using BankManager.Models;

namespace BankManager.Interfaces
{
    // INTERFACE: defines what every transactable account must be able to do.
    // Account implements it, so Checking, Savings, and CD all get it through
    // inheritance.
    public interface ITransactable
    {
        string AccountNumber { get; }
        decimal Balance { get; }

        // Adds money to the account
        TransactionResult Deposit(decimal amount);

        // Checks a withdrawal against the account's rules without changing
        // anything. Fails with the reason if it isn't allowed; otherwise
        // succeeds with any fee or penalty and a warning to show the user.
        TransactionResult ValidateWithdrawal(decimal amount);

        // Removes money from the account using the account's own rules
        TransactionResult Withdraw(decimal amount);
    }
}
