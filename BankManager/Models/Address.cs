/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
 *
 * Purpose: Address holds a customer's mailing address. It has no meaning
 *          on its own in this app; it exists as a part of a Customer, which
 *          makes it one half of the composition demonstration.
 */

namespace BankManager.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address(string street, string city, string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        // Single-line form used in tables and detail views
        public override string ToString()
        {
            return $"{Street}, {City}, {State} {ZipCode}";
        }
    }
}
