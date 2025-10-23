


using CashApp.domen;
using System.Linq;
using CashApp.domen;
using CashApp.domen.Account;
using CashApp.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CashApp.services
{
    /* public class Accountservices : IAccountServices

     {
         public IBankAccount CreateAccount(string name, string currency, decimal initialBalance)
         {
             throw new NotImplementedException();
         }

         public List<IBankAccount> GetAccounts()
         {
             throw new NotImplementedException();
         }
     }
 }
    */

    public partial class Accountservices : IAccountServices 
    {
        private const string StorageKey = "bankapp_accounts";
        //private readonly List<IBankAccount> _accounts= new ();
        private readonly List<Bankacount> _accounts = new();
        private readonly IStorageService _storageService;
        private bool isLoaded;



        public Accountservices(IStorageService storageService) => _storageService = storageService;
        

        private async Task IsInitialized()
        {

            if (isLoaded)
            {
                return;

            }
            var fromStorage = await _storageService.GetItemAsync<List<Bankacount>>(StorageKey);
            _accounts.Clear();
            if (fromStorage is { Count: > 0 }) 
                _accounts.AddRange(fromStorage);
                isLoaded = true;
            
        }
            

        private  Task saveAsync()=> _storageService.SetItemAsync(StorageKey, _accounts);









        public async Task<IBankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {    
            await IsInitialized();

            var account = new Bankacount(name, accountType, currency, initialBalance);
            _accounts.Add(account);

            await saveAsync();
            return account;
        }

        public async Task<List<IBankAccount>> GetAccounts()
        { 
            await IsInitialized();
           // return _accounts.ToList();
            //  return _accounts<IBankAccount>().ToList();
            //return new List<IBankAccount>(_accounts);
            return _accounts.Cast<IBankAccount>().ToList();
        }




        public void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
           var fromAccount = _accounts.OfType<Bankacount>().FirstOrDefault(x =>x.Id == fromAccountId)
            ?? throw new KeyNotFoundException("Account with ID {fromAccountId} not found");
            var  toAccount =_accounts.OfType<Bankacount>().FirstOrDefault(y =>y.Id == toAccountId)
                 ?? throw new KeyNotFoundException("Account with ID {toAccountId} not found"); 
            fromAccount.TransferTo(toAccount, amount);
        }







        public async Task DepositAsync(Guid accountId, decimal amount, string? description = null)
        {
            await IsInitialized();

            if (amount <= 0)
                throw new ArgumentException("Belopp måste vara positivt.", nameof(amount));

            var acc = _accounts.FirstOrDefault(a => a.Id == accountId)
                ?? throw new KeyNotFoundException($"Account with ID {accountId} not found");

            acc.Deposit(amount);
            await saveAsync();
        }
        public async Task WithdrawAsync(Guid accountId, decimal amount, string? description = null)
        {
            await IsInitialized();

            if (amount <= 0)
                throw new ArgumentException("Belopp måste vara positivt.", nameof(amount));

            var acc = _accounts.FirstOrDefault(a => a.Id == accountId)
                ?? throw new KeyNotFoundException($"Account with ID {accountId} not found");

            acc.Withdraw(amount);
            await saveAsync();
        }






        public async Task TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount, string? description = null)
        {
            await IsInitialized();

            if (fromAccountId == toAccountId)
                throw new ArgumentException("Välj två olika konton.");

            if (amount <= 0)
                throw new ArgumentException("Belopp måste vara positivt.", nameof(amount));

            var fromAccount = _accounts.FirstOrDefault(x => x.Id == fromAccountId)
                ?? throw new KeyNotFoundException($"Account with ID {fromAccountId} not found");

            var toAccount = _accounts.FirstOrDefault(y => y.Id == toAccountId)
                ?? throw new KeyNotFoundException($"Account with ID {toAccountId} not found");

            // Utför överföringen med befintliga metoder (validerar även saldo)
            fromAccount.TransferTo(toAccount, amount);

            await saveAsync();
        }

        public Task<IEnumerable<IBankAccount>> GetAccountsAsync()
        {
            throw new NotImplementedException();
        }
    }
}