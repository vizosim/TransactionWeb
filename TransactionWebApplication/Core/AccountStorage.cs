using System;
using System.Threading.Tasks;
using Nito.AsyncEx;
using TransactionWebApplication.Entities;

namespace TransactionWebApplication.Core
{
    public class AccountStorage
    {
        private readonly AsyncReaderWriterLock _transactionLock = new AsyncReaderWriterLock();

        private readonly Account _userAccount = new Account();

        public async Task<decimal> ReadAmountTransaction()
        {
            using (await _transactionLock.ReaderLockAsync())
            {
                decimal amount = _userAccount.Balance;
                return amount;
            }
        }

        public async Task<OperationResult> WriteTransaction(Func<Transaction, Account, OperationResult> transactionFunc,
            Transaction transaction)
        {
            using (await _transactionLock.WriterLockAsync())
            {
                return transactionFunc(transaction, _userAccount);
            }
        }
    }
}
