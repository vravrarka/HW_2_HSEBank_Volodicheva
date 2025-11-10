using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Strategies
{
    public class ConservativeRecalculationStrategy : IBalanceRecalculationStrategy
    {
        public int RecalculateBalance(int accountId, IEnumerable<Operation> operations)
        {
            var accountOperations = operations.Where(o => o.BankAccountId == accountId);
            int balance = 0;
            foreach (var operation in accountOperations)
            {
                if (operation.Type == CategoryType.Income)
                    balance += operation.Amount;
                else
                    balance -= operation.Amount;
                if (balance < 0) balance = 0; 
            }
            return balance;
        }
    }
}
