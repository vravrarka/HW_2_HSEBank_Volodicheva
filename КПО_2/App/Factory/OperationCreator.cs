using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Factory
{
    public class OperationCreator : ObjectCreator
    {
        private readonly CategoryType _type;
        private readonly int _amount;
        private readonly string? _description;
        private readonly DateOnly _date;
        private readonly int _categoryId;
        private readonly int _bankAccountId;
        public OperationCreator(CategoryType type, int bankAccountId, int amount, DateOnly date, int categoryId, string? description = null)
        {
            _type = type;
            _amount = amount;
            _description = description;
            _date = date;
            _categoryId = categoryId;
            _bankAccountId = bankAccountId;
        }
        public override ObjectId FactoryMethod()
        {
            if (_amount <= 0) throw new ArgumentOutOfRangeException("Amount must be zero or more");
            return new Operation
            {
                Type = _type,
                Amount = _amount,
                Description = _description,
                Date = _date,
                CategoryId = _categoryId,
                BankAccountId = _bankAccountId
            };
        }
    }
}