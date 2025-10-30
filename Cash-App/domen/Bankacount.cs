﻿
namespace CashApp.domen;

using CashApp.domen.Account;
using System.Data;
using System.Security.Principal;
using System.Text.Json.Serialization;
using System.Transactions;
using Transaction = Account.Transaction;

public class Bankacount : IBankAccount
{
    //private object _transactionss;

    public Guid Id { get; private set; } = Guid.NewGuid();

    public decimal Balance  { get; private set; }

    public string Name { get; private set; }

    public AccountType AccountType { get; private set; }

    public string Currency { get; private set; }

    public DateTime LastUpdated { get; private set; }

   
  
  
   
   
   
   
   
   private  List<Transaction> _transactions = new();
   public IReadOnlyList<Transaction> Transactions => _transactions;
   
   [JsonInclude]
   public List<Transaction> PersistedTransactions
   {
       get => _transactions;
       private set => _transactions = value ?? new List<Transaction>();
   }
 
   public Bankacount(string name, AccountType accountType, string currency, decimal initialBalance)
    {
        Name = name;
        AccountType = accountType;
        Currency = currency;
        Balance = initialBalance;
        LastUpdated = DateTime.Now;
      
       _transactions.Add(new Transaction
       {
           Date = DateTime.UtcNow,
           Amount = initialBalance,
           BalanceAfter = Balance,
           FromAccountId = Id,
           ToAccountId = Id,
           TransactionType = TransactionType.Deposit,
           Description = "Initial insättning"
       });
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
       // Transactions = new List<Transaction>();
       // Transaction = transactions ?? new List<Transaction>();
    }
    
    
   
   
    public void Deposit(decimal amount) 
    {  Balance += amount;
        _transactions.Add(new Transaction
        {
            Date = DateTime.UtcNow,            // <-- HÄR
            Amount = amount,
            BalanceAfter = Balance,
            FromAccountId = Id,
            ToAccountId = Id,
            TransactionType = TransactionType.Deposit,
            Description = "Insättning"
        });

    }

    public void Withdraw(decimal amount)
    {
        Balance -= amount;
        _transactions.Add(new Transaction
        {
            Date = DateTime.UtcNow,            // <-- HÄR
            Amount = amount,
            BalanceAfter = Balance,
            FromAccountId = Id,
            ToAccountId = Id,
            TransactionType = TransactionType.Withdraw,
            Description = "Uttag"
        }); 
    }

    public void TransferTo(Bankacount toAccount, decimal amount)
    { 
        // från vilket konto
        Balance -= amount;
        LastUpdated = DateTime.UtcNow;

        _transactions.Add(new Transaction
        {    
            TransactionType = TransactionType.TransferOut,
            Amount = amount,
            BalanceAfter = Balance,
           
            FromAccountId = Id,
            ToAccountId = toAccount.Id,
            Date = DateTime.UtcNow,              // <-- ÄNDRING: sätt datum här
            Description = $"Överföring till {toAccount.Name}",
        });

        // till vilket konto
        toAccount.Balance += amount;
        toAccount.LastUpdated = DateTime.UtcNow;
        toAccount._transactions.Add(new Transaction
        {
            TransactionType = TransactionType.TransferIn,
            Amount = amount,
            //BalanceAfter = Balance,  orgi
            BalanceAfter = toAccount.Balance,  
            FromAccountId = Id,
            ToAccountId = toAccount.Id,
            Date = DateTime.UtcNow,              // <-- ÄNDRING: sätt datum här
            Description = $"Överföring från {Name}"
        });
    }
}