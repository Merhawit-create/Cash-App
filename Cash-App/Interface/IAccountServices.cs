using CashApp.domen;
using CashApp.domen.Account;
namespace CashApp.Interface;
using System.Threading.Tasks;
public interface IAccountServices
{
    Task <IBankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance);

    Task<List<IBankAccount>> GetAccounts();
   
}
