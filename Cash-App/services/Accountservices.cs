


using CashApp.domen;
using CashApp.domen;
using CashApp.domen.Account;


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

    public class Accountservices : IAccountServices
    {

        private readonly List<IBankAccount> _accounts= new List<IBankAccount>();
        public IBankAccount CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {
            var account = new Bankacount(name, accountType, currency, initialBalance);
            _accounts.Add(account);
            return account;
        }

        public List<IBankAccount> GetAccounts()
        {
           return new List<IBankAccount>(_accounts);
        }
    }
}