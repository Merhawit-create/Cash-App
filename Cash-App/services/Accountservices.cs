


using CashApp.domen;
using System.Linq;
using CashApp.domen;
using CashApp.domen.Account;
using CashApp.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CashApp.services
{
    /// <summary>
    /// This service manages accounts, money actions, and saving to local storage.
    /// </summary>
    public partial class Accountservices : IAccountServices 
    {
        private const string StorageKey = "bankapp_accounts";
        private readonly List<Bankacount> _accounts = new();
        private readonly IStorageService _storageService;
        private bool isLoaded; 
        public Accountservices(IStorageService storageService) => _storageService = storageService;
        
        /// <summary>
        /// Load accounts from storage once.
        /// </summary>
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
            
        /// <summary>
        /// Save accounts to storage.
        /// </summary>
        private  Task saveAsync()=> _storageService.SetItemAsync(StorageKey, _accounts);




        /// <summary>
        /// Make a new account and save it.
        /// </summary>
        public async Task<IBankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {    
            await IsInitialized();
            var account = new Bankacount(name, accountType, currency, initialBalance);
            _accounts.Add(account);
            await saveAsync();
            return account;
        }

        /// <summary>
        /// Get all accounts.
        /// </summary>
        public async Task<List<IBankAccount>> GetAccounts()
        { 
            await IsInitialized();
            return _accounts.Cast<IBankAccount>().ToList();
        }




     /* !!!!!!!!!!!   public void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
           var fromAccount = _accounts.OfType<Bankacount>().FirstOrDefault(x =>x.Id == fromAccountId)
            ?? throw new KeyNotFoundException("Account with ID {fromAccountId} not found");
            var  toAccount =_accounts.OfType<Bankacount>().FirstOrDefault(y =>y.Id == toAccountId)
                 ?? throw new KeyNotFoundException("Account with ID {toAccountId} not found"); 
            fromAccount.TransferTo(toAccount, amount);
        }*/
     
     
     
     
     /// <summary>
     /// Move money from one account to another.
     /// </summary>
     public async Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount) // <-- async Task
     {
         await IsInitialized();

         var fromAccount = _accounts.FirstOrDefault(x => x.Id == fromAccountId)
                           ?? throw new KeyNotFoundException($"Account with ID {fromAccountId} not found");

         var toAccount = _accounts.FirstOrDefault(y => y.Id == toAccountId)
                         ?? throw new KeyNotFoundException($"Account with ID {toAccountId} not found");

         fromAccount.TransferTo(toAccount, amount);

         await saveAsync(); // <-- VIKTIGT: spara transaktionerna
     }



     /// <summary>
     /// Add money to an account.
     /// </summary>
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
     
        /// <summary>
        /// Take money from an account.
        /// </summary>
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


        /// <summary>
        /// Move money from one account to another with a note.
        /// </summary>
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
          
            
            // Move the money from the sender account to the receiver account
            fromAccount.TransferTo(toAccount, amount); 
            await saveAsync();
        }

        // maybe kelgso ye
        public Task<IEnumerable<IBankAccount>> GetAccountsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a transaction from one account.
        /// </summary>
        public async Task DeleteTransactionAsync(Guid accountId, Guid transactionId)
        {
            await IsInitialized();
            var acc = _accounts.FirstOrDefault(a => a.Id == accountId)
                      ?? throw new KeyNotFoundException($"Account with ID {accountId} not found");

            // Check if 'acc' is a Bankacount object and, if it is, create a variable 'bankAcc' that refers to it
            if (acc is Bankacount bankAcc)
            { 
                var removed = bankAcc.RemoveTransaction(transactionId);
                if (!removed) throw new KeyNotFoundException($"Transaction {transactionId} not found");
                await saveAsync();
            }
            else
            { 
                throw new InvalidOperationException("Unexpected account type.");
            }
        }

       
        private const string CorrectPin = "1234";
        public Task<bool> ValidatePinAsync(string pin)
            => Task.FromResult(pin == CorrectPin);
     
    }
}