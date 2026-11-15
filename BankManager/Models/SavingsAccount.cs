/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
 *
 * Purpose: SavingsAccount is a DERIVED CLASS of Account. It adds an annual
 *          interest rate and a cap on withdrawals per month.
 */

namespace BankManager.Models
{
    // INHERITANCE: SavingsAccount "is an" Account
    public class SavingsAccount : Account
    {
        public decimal InterestRate { get; }          // annual rate, 0.0425 = 4.25%
        public int MonthlyWithdrawalLimit { get; }

        // INHERITANCE: ": base(...)" passes the shared fields up to Account's constructor
        public SavingsAccount(string accountNumber, decimal openingBalance, DateTime dateOpened,
                              decimal interestRate, int monthlyWithdrawalLimit)
            : base(accountNumber, openingBalance, dateOpened)
        {
            InterestRate = interestRate;
            MonthlyWithdrawalLimit = monthlyWithdrawalLimit;
        }

        // INHERITANCE: overrides the base class member
        public override string AccountType => "Savings";

        public override string GetFeatureSummary()
        {
            return $"{InterestRate.ToString("P2", Usd)} APY, {MonthlyWithdrawalLimit} withdrawals/mo";
        }
    }
}
