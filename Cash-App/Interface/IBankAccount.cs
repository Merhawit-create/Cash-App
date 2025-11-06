using CashApp.domen;
using CashApp.domen.Account;
namespace CashApp.Interface;   

/// <summary>
/// Defines the structure and behavior of a bank account, including properties for account details
/// and operations for depositing, withdrawing, transferring funds, and applying interest.
/// </summary>

public interface IBankAccount
{
    Guid Id { get; }
    public decimal Balance { get;  }
    string Name { get;  }
    public AccountType AccountType { get; }
    string Currency { get; }
    DateTime LastUpdated { get;  }
    IReadOnlyList<Transaction> Transactions { get; }
    decimal? InterestRate { get;  }
    void Withdraw(decimal amount);
    void Deposit(decimal amount);
    void TransferTo(Bankacount toAccount, decimal amount);
    void ApplyInterest();
}

