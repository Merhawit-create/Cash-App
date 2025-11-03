using CashApp.domen;
using CashApp.domen.Account;
namespace CashApp.Interface;   
/// <summary>
/// Defines what a bank account can do.
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
    void Withdraw(decimal amount);
    void Deposit(decimal amount);
    void TransferTo(Bankacount toAccount, decimal amount);
}

