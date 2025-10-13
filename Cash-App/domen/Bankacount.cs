
namespace CashApp.domen;

using CashApp.domen.Account;
using System.Data;
using System.Security.Principal;

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


    public void Deposit(decimal amount)
    {
        throw new NotImplementedException();
    }

    public void Withdraw(decimal amount)
    {
        throw new NotImplementedException();
    }
}

