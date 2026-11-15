/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
 *
 * Purpose: CertificateOfDeposit is a DERIVED CLASS of Account. It locks the
 *          balance for a fixed term at a fixed rate and tracks its maturity
 *          date.
 */

namespace BankManager.Models
{
    // INHERITANCE: CertificateOfDeposit "is an" Account
    public class CertificateOfDeposit : Account
    {
        public decimal InterestRate { get; }
        public int TermMonths { get; }

        public DateTime MaturityDate => DateOpened.AddMonths(TermMonths);

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
            return $"{TermMonths}-mo term at {InterestRate.ToString("P2", Usd)}, matures {MaturityDate:MM/dd/yyyy}";
        }
    }
}
