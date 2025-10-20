using CashApp.domen;
using CashApp.domen.Account;
namespace CashApp.Interface;    
public interface IBankAccount
{
    Guid Id { get; }

    public decimal Balance { get;  }
    string Name { get;  }
    public AccountType AccountType { get; }
    string Currency { get; }
    DateTime LastUpdated { get;  }


    void Withdraw(decimal amount);
    void Deposit(decimal amount);
    void TransferTo(Bankacount toAccount, decimal amount);
}

