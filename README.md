# Cedar Ledger Bank Account Manager

SDC320L Advanced Object-Oriented Programming Using C# Lab, course project.
Addison Smith (addsmi1720)

A C# console application for managing customers and their bank accounts
(Checking, Savings, and Certificate of Deposit). Built up over five weekly
phases, ending with SQLite storage.

## Phases

- Phase 1: user interaction design, inheritance, composition
- Phase 2: polymorphism, interface
- Phase 3: constructors, access specifiers, abstract class
- Phase 4: SQLite database
- Phase 5: final submission

## Run

Requires the .NET 8 SDK.

```
cd BankManager
dotnet run
```

## Where the Week 1 concepts are

- Inheritance: `Models/Account.cs` (base) with `CheckingAccount.cs`,
  `SavingsAccount.cs`, and `CertificateOfDeposit.cs` (derived)
- Composition: `Models/Customer.cs` (has an Address and a list of Accounts)
  and `Models/Bank.cs` (has Customers)
- Reusable UI: `UI/ConsoleUI.cs`, `UI/InputHelper.cs`, `UI/TableFormatter.cs`
