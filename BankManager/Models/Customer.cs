/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
 *
 * Purpose: Customer is the COMPOSITION class. A customer is built from
 *          other objects: one Address and a list of Accounts. The customer
 *          creates and owns its account list, and the accounts are reached
 *          through the customer.
 */

namespace BankManager.Models
{
    public class Customer
    {
        public int CustomerId { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // COMPOSITION: a Customer "has an" Address
        public Address MailingAddress { get; set; }

        // COMPOSITION: a Customer "has" Accounts. The list is created here and
        // owned by the customer; outside code reads it but cannot replace it.
        private readonly List<Account> _accounts = new List<Account>();
        public IReadOnlyList<Account> Accounts => _accounts;

        public Customer(int customerId, string firstName, string lastName,
                        string phone, string email, Address mailingAddress)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            MailingAddress = mailingAddress;
        }

        public string FullName => $"{FirstName} {LastName}";

        // Any Account subtype can be added because each one "is an" Account
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public decimal TotalBalance()
        {
            return _accounts.Sum(a => a.Balance);
        }
    }
}
