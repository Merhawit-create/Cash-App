﻿
namespace CashApp.domen;

using CashApp.domen.Account;
using System.Data;
using System.Security.Principal;
using System.Text.Json.Serialization;
using System.Transactions;
using Transaction = Account.Transaction;

/// <summary>
/// Represents a simple bank account with balance tracking and a local transaction ledger.
/// </summary>

public class Bankacount : IBankAccount
{
   
    public Guid Id { get; private set; } = Guid.NewGuid();
    public decimal Balance  { get; private set; }
    public string Name { get; private set; } 
    public AccountType AccountType { get; private set; }
    public string Currency { get; private set; }
    public DateTime LastUpdated { get; private set; }
    public decimal? InterestRate { get; private set; }
    private  List<Transaction> _transactions = new();
   public IReadOnlyList<Transaction> Transactions => _transactions;
   
   /// <summary>
   /// Helper property for System.Text.Json to persist and restore transactions.
   /// </summary>
   [JsonInclude]
   public List<Transaction> PersistedTransactions
   {
       get => _transactions;
       private set => _transactions = value ?? new List<Transaction>();
   }

   /// <summary>
   /// Makes a new account with a starting balance and saves a record of the first deposit.
   /// </summary>

  
   public Bankacount(string name, AccountType accountType, string currency, decimal initialBalance,decimal?  interestRate )
    {
        Name = name;
        AccountType = accountType;
        Currency = currency;
        Balance = initialBalance;
        InterestRate = interestRate;
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
    /// <summary>
    /// JSON constructor used when deserializing an account from storage.
    /// </summary>

    [JsonConstructor]
    public Bankacount(Guid id, string name, AccountType accountType, string currency, decimal balance, DateTime lastUpdated,decimal? interestRate)
    {
        Id = id;
        Name = name;
        AccountType = accountType;
        Currency = currency;
        Balance = balance;
        LastUpdated = lastUpdated;
        InterestRate = interestRate;
      
    }

    /// <summary>
    /// Deposits a positive amount into this account and records a transaction.
    /// </summary>
    /// <param name="amount">Amount to deposit.</param>
    /// <param name="ränteinsättning"></param>
    public void Deposit(decimal amount, string ränteinsättning) 
    {  Balance += amount;
        _transactions.Add(new Transaction
        {
            Date = DateTime.UtcNow,            
            Amount = amount,
            BalanceAfter = Balance,
            FromAccountId = Id,
            ToAccountId = Id,
            TransactionType = TransactionType.Deposit,
            Description = "Insättning"
        });

    }
    /// <summary>
    /// Withdraws an amount from this account and records a transaction.
    /// </summary>
    /// <param name="amount">Amount to withdraw.</param>
    public void Withdraw(decimal amount)
    {
        Balance -= amount;
        _transactions.Add(new Transaction
        {
            Date = DateTime.UtcNow,            
            Amount = amount,
            BalanceAfter = Balance,
            FromAccountId = Id,
            ToAccountId = Id,
            TransactionType = TransactionType.Withdraw,
            Description = "Uttag"
        }); 
    }
    /// <summary>
    /// Deposits the specified amount into the account by increasing the current balance.
    /// </summary>
    public void Deposit(decimal amount)
    {
        Balance += amount;
    }
    
    /// <summary>
    /// Transfers funds from this account to another account and records both sides of the transfer.
    /// </summary>
    /// <param name="toAccount">Destination account.</param>
    /// <param name="amount">Amount to transfer.</param>
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
            Date = DateTime.UtcNow,             
            Description = $"Överföring till {toAccount.Name}",
        });

        // till vilket konto
        toAccount.Balance += amount;
        toAccount.LastUpdated = DateTime.UtcNow;
        toAccount._transactions.Add(new Transaction
        {
            TransactionType = TransactionType.TransferIn,
            Amount = amount,
            BalanceAfter = toAccount.Balance,  
            FromAccountId = Id,
            ToAccountId = toAccount.Id,
            Date = DateTime.UtcNow,              
            Description = $"Överföring från {Name}"
        });
    }
    /// <summary>
    /// Deletes one transaction from the account’s list of transactions using its ID.
    /// </summary>
   
    public bool RemoveTransaction(Guid transactionId)  
    {
        var tx = _transactions.FirstOrDefault(t => t.Id == transactionId);
        if (tx is null) return false;
        _transactions.Remove(tx);
        return true;
    }
    
    /// <summary>
    /// Applies interest to the account if it is a savings account and has a valid interest rate.
    /// The calculated interest is added to the balance as a deposit.
    /// </summary>
    
    public void ApplyInterest()
    {
        if (AccountType == AccountType.Savings && InterestRate.HasValue && InterestRate > 0)
        {
            var interest = Balance * InterestRate.Value;
            Deposit(interest, "Ränteinsättning");
        }
    }
    
}

