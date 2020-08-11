namespace TransactionWebApplication.Entities
{
    public class TransactionLog : Transaction
    {
        public TransactionLog(Transaction transaction)
        {
            Id = transaction.Id;
            Type = transaction.Type;
            Status = transaction.Status;
            Amount = transaction.Amount;
            EffectiveDate = transaction.EffectiveDate;
        }

        public decimal Balance { get; set; }

        public string Message { get; set; }
    }
}
