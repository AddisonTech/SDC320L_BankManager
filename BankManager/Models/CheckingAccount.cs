/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
 *
 * Purpose: CheckingAccount is a DERIVED CLASS of Account. It adds an
 *          overdraft limit and a monthly fee that is waived above a
 *          minimum balance.
 */

namespace BankManager.Models
{
    // INHERITANCE: CheckingAccount "is an" Account
    public class CheckingAccount : Account
    {
        public decimal OverdraftLimit { get; }
        public decimal MonthlyFee { get; }
        public decimal FeeWaiverBalance { get; }

        // INHERITANCE: ": base(...)" passes the shared fields up to Account's constructor
        public CheckingAccount(string accountNumber, decimal openingBalance, DateTime dateOpened,
                               decimal overdraftLimit, decimal monthlyFee, decimal feeWaiverBalance)
            : base(accountNumber, openingBalance, dateOpened)
        {
            OverdraftLimit = overdraftLimit;
            MonthlyFee = monthlyFee;
            FeeWaiverBalance = feeWaiverBalance;
        }

        // INHERITANCE: overrides the base class member
        public override string AccountType => "Checking";

        public override string GetFeatureSummary()
        {
            string fee = Balance >= FeeWaiverBalance
                ? "fee waived"
                : $"{MonthlyFee.ToString("C", Usd)}/mo fee";
            return $"Overdraft {OverdraftLimit.ToString("C0", Usd)}, {fee}";
        }
    }
}
