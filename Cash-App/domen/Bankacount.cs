
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

    public Guid Id { get; private set; }

    public decimal Balance  { get; private set; }

    public string Name { get; private set; }

    public AccountType AccountType { get; private set; }

    public string Currency { get; private set; }

    public DateTime LastUpdated { get; private set; }

   public List<Transaction> _transactions { get; private set; } = new();


    public Bankacount(string name, AccountType accountType, string currency, decimal initialBalance)
    {
        Name = name;
        AccountType = accountType;
        Currency = currency;
        Balance = initialBalance;
        LastUpdated = DateTime.Now;
       /* Transactions = new List<Transaction>
    {
        new Transaction
        {
            Amount = initialBalance,
            Description = "Initial Deposit",
            Type = TransactionType.Deposit,
            FromAccountId = Id
        }
    };*/
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
    
    
   
   
    public void Deposit(decimal amount) { 

    }

    public void Withdraw(decimal amount)
    {
       
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
        });

        // till vilket konto
        toAccount.Balance += amount;
        toAccount.LastUpdated = DateTime.UtcNow;
        toAccount._transactions.Add(new Transaction
        {
            TransactionType = TransactionType.TransferIn,
            Amount = amount,
            BalanceAfter = Balance,
            FromAccountId = Id,
            ToAccountId = toAccount.Id,
        });
    }
}

