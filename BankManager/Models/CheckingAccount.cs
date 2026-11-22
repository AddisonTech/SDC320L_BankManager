/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/22/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 2
 *
 * Purpose: CheckingAccount is a DERIVED CLASS of Account. It adds an
 *          overdraft limit and a monthly fee that is waived above a
 *          minimum balance. Withdrawals may take the balance below zero, down
 *          to the overdraft limit, with a fee for each overdraft.
 */

namespace BankManager.Models
{
    // INHERITANCE: CheckingAccount "is an" Account
    public class CheckingAccount : Account
    {
        public const decimal OverdraftFee = 35m;

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

        // POLYMORPHISM: Checking's withdrawal rules. Unlike the base rule, the
        // balance may go negative down to the overdraft limit, with an
        // overdraft fee charged when that happens.
        public override TransactionResult ValidateWithdrawal(decimal amount)
        {
            if (amount <= 0)
            {
                return TransactionResult.Fail("Withdrawal amount must be greater than zero.");
            }

            if (amount <= Balance)
            {
                return TransactionResult.Ok(string.Empty);
            }

            if (Balance - amount - OverdraftFee < -OverdraftLimit)
            {
                return TransactionResult.Fail(
                    $"That would pass the {Money(OverdraftLimit)} overdraft limit. Most you can withdraw: {Money(MaxWithdrawal())}");
            }

            return TransactionResult.Ok(
                $"This overdraws the account. A {Money(OverdraftFee)} overdraft fee will be charged.",
                OverdraftFee, "overdraft fee");
        }

        // Largest withdrawal allowed, accounting for the overdraft fee
        private decimal MaxWithdrawal()
        {
            decimal withoutFee = Math.Max(0m, Balance);
            decimal withFee = Balance + OverdraftLimit - OverdraftFee;
            return Math.Max(withoutFee, withFee);
        }
    }
}
