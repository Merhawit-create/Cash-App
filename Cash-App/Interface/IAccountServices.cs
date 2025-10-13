using CashApp.domen;
using CashApp.domen.Account;
namespace CashApp.Interface;    
public interface IAccountServices
{
    IBankAccount CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance);

    List<IBankAccount> GetAccounts();
}
