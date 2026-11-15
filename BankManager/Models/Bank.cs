/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
 *
 * Purpose: Bank is the top-level container for the application. It is
 *          composed of Customers, which are in turn composed of an Address
 *          and Accounts. In Week 4 this class is where the SQLite storage
 *          will plug in.
 */

namespace BankManager.Models
{
    public class Bank
    {
        public string Name { get; }

        // COMPOSITION: a Bank "has" Customers
        private readonly List<Customer> _customers = new List<Customer>();
        public IReadOnlyList<Customer> Customers => _customers;

        public Bank(string name)
        {
            Name = name;
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
        }

        public Customer? FindCustomer(int customerId)
        {
            return _customers.FirstOrDefault(c => c.CustomerId == customerId);
        }
    }
}
