using CashApp.domen;
using CashApp.domen.Account;
namespace CashApp.Interface;
using System.Threading.Tasks;
public interface IAccountServices
{
    Task <IBankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance);

    Task<List<IBankAccount>> GetAccounts();
    void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);

    Task DepositAsync(Guid accountId, decimal amount, string? description = null);
    Task WithdrawAsync(Guid accountId, decimal amount, string? description = null);
    Task TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount, string? description = null);
    //Task<object> GetAccountsAsync()
    Task<IEnumerable<IBankAccount>> GetAccountsAsync();
}
