
namespace CashApp.domen;

using CashApp.domen.Account;
using System.Data;
using System.Security.Principal;
using System.Text.Json.Serialization;

public class Bankacount : IBankAccount
{


    public Guid Id { get; private set; }

    public decimal Balance  { get; private set; }

    public string Name { get; private set; }

    public AccountType AccountType { get; private set; }

    public string Currency { get; private set; }

    public DateTime LastUpdated { get; private set; }


    public Bankacount(string name, AccountType accountType, string currency, decimal initialBalance)
    {
        Name = name;
        AccountType = accountType;
        Currency = currency;
        Balance = initialBalance;
        LastUpdated = DateTime.Now;
    }


    [JsonConstructor]
    public Bankacount(Guid id, string name, AccountType accountType, string currency, decimal balance, DateTime lastUpdated)
    {
        Id = id;
        Name = name;
        AccountType = accountType;
        Currency = currency;
        Balance = balance;
        LastUpdated = lastUpdated;
    }
    
    
   
   
    public void Deposit(decimal amount) { 
    if (amount <= 0)
        throw new ArgumentException("Deposit amount must be positive.");

    Balance += amount;
    LastUpdated = DateTime.Now;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdraw amount must be positive.");

        if (amount > Balance)
            throw new InvalidOperationException("Insufficient balance.");

        Balance -= amount;
        LastUpdated = DateTime.Now;
    }
}

