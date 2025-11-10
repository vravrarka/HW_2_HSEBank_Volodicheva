using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Strategies
{
    public class SimpleRecalculationStrategy : IBalanceRecalculationStrategy
    {
        public int RecalculateBalance(int accountId, IEnumerable<Operation> operations)
        {
            var accountOperations = operations.Where(o => o.BankAccountId == accountId);
            return accountOperations.Sum(o => o.Type == CategoryType.Income ? o.Amount : -o.Amount);
        }
    }
}
