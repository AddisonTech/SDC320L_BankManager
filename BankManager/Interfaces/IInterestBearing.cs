/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/22/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 2
 *
 * Purpose: IInterestBearing is the contract for accounts that earn interest.
 *          Only some account types earn interest (Savings and CD do, Checking
 *          does not), so this is an interface instead of a member of the
 *          Account base class. The month-end interest screen works on any
 *          object that implements it.
 */

using BankManager.Models;

namespace BankManager.Interfaces
{
    // INTERFACE: implemented by SavingsAccount and CertificateOfDeposit only
    public interface IInterestBearing
    {
        // Annual rate as a decimal, 0.0425 = 4.25%
        decimal InterestRate { get; }

        // Interest this account will earn for one month at its current balance
        decimal CalculateMonthlyInterest();

        // Posts one month of interest to the balance
        TransactionResult PostMonthlyInterest();
    }
}
