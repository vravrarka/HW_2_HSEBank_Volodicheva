using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Strategies
{
    public interface IBalanceRecalculationStrategy
    {
        int RecalculateBalance(int accountId, IEnumerable<Operation> operations);
    }
}
