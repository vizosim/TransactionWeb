using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionWebApplication.Entities;

namespace TransactionWebApplication.Core
{
    public class TransactionLogStore
    {
        private readonly ConcurrentDictionary<string, TransactionLog> _log = new ConcurrentDictionary<string, TransactionLog>();

        public Task<IEnumerable<TransactionLog>> GeTransactionLog()
        {
            IEnumerable<TransactionLog> result = _log.Values.OrderByDescending(_ => _.EffectiveDate);
            return Task.FromResult(result);
        }

        public Task<TransactionLog> GeTransaction(string id)
        {
            if (_log.TryGetValue(id, out var transaction))
                return Task.FromResult(transaction);
            return null;
        }

        public void AddLog(TransactionLog transaction)
        {
            _log.AddOrUpdate(transaction.Id, transaction, (id, log) => transaction);
        }
    }
}