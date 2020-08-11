namespace TransactionWebApplication.Entities
{
    public class OperationResult
    {
        public OperationResult(string transactionId, TransactionStatus status, string message = null)
        {
            TransactionId = transactionId;
            Status = status;
            Message = message;
        }
        public OperationResult(string transactionId, TransactionStatus status, decimal result)
        {
            TransactionId = transactionId;
            Status = status;
            Result = result;
        }

        public TransactionStatus Status { get; private set; }

        public string Message { get; private set; }

        public string TransactionId { get; set; }

        public decimal Result { get; }
    }
}