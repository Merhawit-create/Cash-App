namespace CashApp.domen.Account
{
    /// <summary>
    /// One record of money movement in or out of an account.
    /// </summary>
    public class Transaction
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public decimal BalanceAfter { get; set; }
        public Guid? FromAccountId { get; set; }
        public Guid? ToAccountId { get; set; } 
        public TransactionType Type { get; set; }
        public string Description { get; set; } 
        public TransactionType TransactionType { get; internal set; }
      
    }

    /// <summary>
    /// The type of transaction: deposit, withdraw, or transfer.
    /// </summary>
    public enum TransactionType
    {
        Deposit,
        Withdraw,
        TransferIn,
        TransferOut 
    }

}
