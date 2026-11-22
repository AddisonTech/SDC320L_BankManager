/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/22/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 2
 *
 * Purpose: TransactionResult reports what happened (or would happen) when a
 *          deposit, withdrawal, or interest posting is attempted: whether it
 *          is allowed, a message for the user, and any fee or penalty with
 *          its name.
 */

namespace BankManager.Models
{
    public class TransactionResult
    {
        public bool Success { get; }
        public string Message { get; }
        public decimal FeeCharged { get; }
        public string FeeName { get; }

        public TransactionResult(bool success, string message, decimal feeCharged = 0m, string feeName = "")
        {
            Success = success;
            Message = message;
            FeeCharged = feeCharged;
            FeeName = feeName;
        }

        public static TransactionResult Ok(string message, decimal fee = 0m, string feeName = "")
            => new TransactionResult(true, message, fee, feeName);

        public static TransactionResult Fail(string message)
            => new TransactionResult(false, message);
    }
}
