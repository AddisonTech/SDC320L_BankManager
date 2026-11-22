/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/22/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 2
 *
 * Purpose: SavingsAccount is a DERIVED CLASS of Account and implements the
 *          IInterestBearing interface. It earns interest monthly, does not
 *          allow overdrafts, and caps the number of withdrawals per month.
 */

using BankManager.Interfaces;

namespace BankManager.Models
{
    // INHERITANCE: SavingsAccount "is an" Account
    // INTERFACE: SavingsAccount implements IInterestBearing
    public class SavingsAccount : Account, IInterestBearing
    {
        public decimal InterestRate { get; }          // annual rate, 0.0425 = 4.25%
        public int MonthlyWithdrawalLimit { get; }
        public int WithdrawalsThisMonth { get; private set; }

        // INHERITANCE: ": base(...)" passes the shared fields up to Account's constructor
        public SavingsAccount(string accountNumber, decimal openingBalance, DateTime dateOpened,
                              decimal interestRate, int monthlyWithdrawalLimit, int withdrawalsThisMonth = 0)
            : base(accountNumber, openingBalance, dateOpened)
        {
            InterestRate = interestRate;
            MonthlyWithdrawalLimit = monthlyWithdrawalLimit;
            WithdrawalsThisMonth = withdrawalsThisMonth;
        }

        // INHERITANCE: overrides the base class member
        public override string AccountType => "Savings";

        public override string GetFeatureSummary()
        {
            return $"{InterestRate.ToString("P2", Usd)} APY, {WithdrawalsThisMonth} of {MonthlyWithdrawalLimit} withdrawals used";
        }

        // POLYMORPHISM: Savings' withdrawal rules. Blocks the withdrawal once
        // the monthly limit is used up, then applies the normal no-overdraft
        // rule from the base class.
        public override TransactionResult ValidateWithdrawal(decimal amount)
        {
            if (WithdrawalsThisMonth >= MonthlyWithdrawalLimit)
            {
                return TransactionResult.Fail(
                    $"Monthly withdrawal limit reached ({MonthlyWithdrawalLimit} of {MonthlyWithdrawalLimit} used). Try again next month.");
            }

            TransactionResult baseCheck = base.ValidateWithdrawal(amount);
            if (!baseCheck.Success)
            {
                return baseCheck;
            }

            int left = MonthlyWithdrawalLimit - WithdrawalsThisMonth;
            return TransactionResult.Ok($"This uses 1 of your {left} remaining withdrawal(s) this month.");
        }

        // POLYMORPHISM: Savings extends the base Withdraw to count the withdrawal
        public override TransactionResult Withdraw(decimal amount)
        {
            TransactionResult result = base.Withdraw(amount);
            if (result.Success)
            {
                WithdrawalsThisMonth++;
            }
            return result;
        }

        // INTERFACE: IInterestBearing members
        public decimal CalculateMonthlyInterest()
        {
            return Math.Round(Balance * InterestRate / 12m, 2);
        }

        public TransactionResult PostMonthlyInterest()
        {
            decimal interest = CalculateMonthlyInterest();
            Balance += interest;
            WithdrawalsThisMonth = 0; // new month resets the withdrawal count
            return TransactionResult.Ok($"Posted {Money(interest)} interest to {AccountNumber}.");
        }
    }
}
