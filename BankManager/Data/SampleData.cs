/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
 *
 * Purpose: SampleData builds the starting set of customers and accounts
 *          with realistic values. It stands in for the SQLite database
 *          until Week 4.
 */

using BankManager.Models;

namespace BankManager.Data
{
    public static class SampleData
    {
        public static Bank CreateBank()
        {
            Bank bank = new Bank("Cedar Ledger Bank");

            // COMPOSITION: each Customer is built with its own Address object,
            // then given its Accounts.
            Customer marcus = new Customer(101, "Marcus", "Whitfield", "(864) 555-0142", "mwhitfield@example.com",
                new Address("118 Laurel Creek Rd", "Belton", "SC", "29627"));
            // INHERITANCE: three different derived types stored as Account
            marcus.AddAccount(new CheckingAccount("CHK-100245", 2847.16m, new DateTime(2021, 3, 8), 500m, 12m, 1500m));
            marcus.AddAccount(new SavingsAccount("SAV-200318", 14250.00m, new DateTime(2021, 3, 8), 0.0425m, 6));
            marcus.AddAccount(new CertificateOfDeposit("CD-300077", 10000.00m, new DateTime(2025, 6, 1), 0.0465m, 24));
            bank.AddCustomer(marcus);

            Customer elena = new Customer(102, "Elena", "Castillo", "(864) 555-0187", "ecastillo@example.com",
                new Address("42 Mill Village Ln", "Williamston", "SC", "29697"));
            elena.AddAccount(new CheckingAccount("CHK-100391", 612.40m, new DateTime(2023, 9, 14), 250m, 12m, 1500m));
            elena.AddAccount(new SavingsAccount("SAV-200455", 3305.72m, new DateTime(2024, 1, 2), 0.0410m, 6));
            bank.AddCustomer(elena);

            Customer darnell = new Customer(103, "Darnell", "Price", "(803) 555-0119", "dprice@example.com",
                new Address("907 Gervais St Apt 3B", "Columbia", "SC", "29201"));
            darnell.AddAccount(new CheckingAccount("CHK-100512", 5120.88m, new DateTime(2019, 11, 20), 1000m, 15m, 2500m));
            darnell.AddAccount(new CertificateOfDeposit("CD-300104", 25000.00m, new DateTime(2026, 2, 15), 0.0480m, 12));
            bank.AddCustomer(darnell);

            Customer june = new Customer(104, "June", "Halvorsen", "(864) 555-0163", "jhalvorsen@example.com",
                new Address("15 Ridgecrest Dr", "Anderson", "SC", "29621"));
            june.AddAccount(new SavingsAccount("SAV-200602", 48900.10m, new DateTime(2018, 5, 30), 0.0425m, 6));
            bank.AddCustomer(june);

            return bank;
        }
    }
}
