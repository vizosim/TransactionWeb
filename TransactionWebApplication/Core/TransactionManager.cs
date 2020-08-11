using System;
using System.Threading.Tasks;
using TransactionWebApplication.Entities;

namespace TransactionWebApplication.Core
{
    public class TransactionManager
    {
        private readonly AccountStorage _storage;
        private readonly TransactionLogStore _transactionLog;

        public TransactionManager(AccountStorage storage, TransactionLogStore transactionLog)
        {
            _storage = storage;
            _transactionLog = transactionLog;
        }

        public async Task<decimal> ReadAmount()
        {
            return await _storage.ReadAmountTransaction();
        }

        public async Task<OperationResult> CommitTransaction(Transaction transaction)
        {
            transaction.Id = Guid.NewGuid().ToString("N");
            transaction.EffectiveDate = DateTime.Now;
            transaction.Status = TransactionStatus.Created;

            _transactionLog.AddLog(new TransactionLog(transaction));

            var result = await _storage.WriteTransaction(ExecuteTransaction, transaction);

            return result;
        }

        private OperationResult ExecuteTransaction(Transaction transaction, Account userAccount)
        {
            try
            {
                transaction.Status = TransactionStatus.InProgress;

                PushTransactionLog(transaction, "Transaction being processing", userAccount.Balance);

                if (!ValidateTransaction(transaction, userAccount))
                {
                    transaction.Status = TransactionStatus.Rejected;

                    string failedMessage = "Validation Failed";
                    PushTransactionLog(transaction, failedMessage, userAccount.Balance);

                    return new OperationResult(transaction.Id, TransactionStatus.Rejected, failedMessage);
                }

                decimal oldAmount = userAccount.Balance;
                decimal newAmount;
                if (transaction.Type == TransactionType.Credit)
                {
                    newAmount = oldAmount + transaction.Amount;
                }
                else
                {
                    newAmount = oldAmount - transaction.Amount;
                }

                userAccount.Balance = newAmount;

                transaction.Status = TransactionStatus.Success;

                PushTransactionLog(transaction, "Transaction processed successfully", userAccount.Balance);

                return new OperationResult(transaction.Id, TransactionStatus.Success);
            }
            catch (Exception e)
            {
                transaction.Status = TransactionStatus.Failed;
                PushTransactionLog(transaction, "Transaction failed", userAccount.Balance);

                return new OperationResult(transaction.Id, TransactionStatus.Failed, e.Message);
            }
        }

        private void PushTransactionLog(Transaction transaction, string message, decimal balance)
        {
            var log = new TransactionLog(transaction);
            log.Balance = balance;
            log.Message = message;

            _transactionLog.AddLog(log);
        }

        private bool ValidateTransaction(Transaction transaction, Account account)
        {
            if (transaction.Type != TransactionType.Debit)
                return true;

            if (account.Balance < transaction.Amount)
                return false;
            return true;
        }
    }
}