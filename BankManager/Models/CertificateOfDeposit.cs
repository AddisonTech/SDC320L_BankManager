/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/22/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 2
 *
 * Purpose: CertificateOfDeposit is a DERIVED CLASS of Account and implements
 *          the IInterestBearing interface. It locks the balance for a fixed
 *          term at a fixed rate. Withdrawing before the maturity date costs
 *          an early-withdrawal penalty of three months' interest on the
 *          amount withdrawn.
 */

using BankManager.Interfaces;

namespace BankManager.Models
{
    // INHERITANCE: CertificateOfDeposit "is an" Account
    // INTERFACE: CertificateOfDeposit implements IInterestBearing
    public class CertificateOfDeposit : Account, IInterestBearing
    {
        public const int PenaltyMonths = 3;

        public decimal InterestRate { get; }
        public int TermMonths { get; }

        public DateTime MaturityDate => DateOpened.AddMonths(TermMonths);
        public bool IsMatured => DateTime.Today >= MaturityDate;

        // INHERITANCE: ": base(...)" passes the shared fields up to Account's constructor
        public CertificateOfDeposit(string accountNumber, decimal openingBalance, DateTime dateOpened,
                                    decimal interestRate, int termMonths)
            : base(accountNumber, openingBalance, dateOpened)
        {
            InterestRate = interestRate;
            TermMonths = termMonths;
        }

        // INHERITANCE: overrides the base class member
        public override string AccountType => "CD";

        public override string GetFeatureSummary()
        {
            string maturity = IsMatured ? $"matured {MaturityDate:MM/dd/yyyy}" : $"matures {MaturityDate:MM/dd/yyyy}";
            return $"{TermMonths}-mo term at {InterestRate.ToString("P2", Usd)}, {maturity}";
        }

        // POLYMORPHISM: the CD's withdrawal rules. Before maturity it adds an
        // early-withdrawal penalty; after maturity it uses the base rule.
        public override TransactionResult ValidateWithdrawal(decimal amount)
        {
            if (IsMatured)
            {
                return base.ValidateWithdrawal(amount);
            }

            if (amount <= 0)
            {
                return TransactionResult.Fail("Withdrawal amount must be greater than zero.");
            }

            decimal penalty = EarlyWithdrawalPenalty(amount);
            if (amount + penalty > Balance)
            {
                return TransactionResult.Fail(
                    $"Insufficient funds. {Money(amount)} plus a {Money(penalty)} penalty is more than the {FormattedBalance} balance.");
            }

            return TransactionResult.Ok(
                $"This CD matures {MaturityDate:MM/dd/yyyy}. Early withdrawal penalty: {Money(penalty)} ({PenaltyMonths} months' interest).",
                penalty, "early-withdrawal penalty");
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
            return TransactionResult.Ok($"Posted {Money(interest)} interest to {AccountNumber}.");
        }

        private decimal EarlyWithdrawalPenalty(decimal amount)
        {
            return IsMatured ? 0m : Math.Round(amount * InterestRate * PenaltyMonths / 12m, 2);
        }
    }
}
