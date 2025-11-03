using CashApp.domen;
using CashApp.domen.Account;
namespace CashApp.Interface;
using System.Threading.Tasks;
/// <summary>
/// This interface has all the account actions (create, get, transfer, etc.).
/// </summary>
public interface IAccountServices
{
    Task <IBankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance);
    Task<List<IBankAccount>> GetAccounts();
    Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
    Task DepositAsync(Guid accountId, decimal amount, string? description = null);
    Task WithdrawAsync(Guid accountId, decimal amount, string? description = null);
    Task TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount, string? description = null);
    Task<IEnumerable<IBankAccount>> GetAccountsAsync(); 
    Task DeleteTransactionAsync(Guid accountId, Guid transactionId); 
    Task<bool> ValidatePinAsync(string pin);
   

}
