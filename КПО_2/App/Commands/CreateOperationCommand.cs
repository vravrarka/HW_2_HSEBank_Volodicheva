using Bank.App.Facade;
using Bank.MainClasses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Commands
{
    public class CreateOperationCommand : Command
    {
        private readonly FinancialAccountingFacade _facade;
        private readonly CategoryType _type;
        private readonly int _bankAccountId;
        private readonly int _amount;
        private readonly DateOnly _date;
        private readonly int _categoryId;
        private readonly string? _description;
        private int _createdOperationId;
        public CreateOperationCommand(FinancialAccountingFacade facade, CategoryType type,
            int bankAccountId, int amount, DateOnly date, int categoryId, string? description = null)
        {
            _facade = facade;
            _type = type;
            _bankAccountId = bankAccountId;
            _amount = amount;
            _date = date;
            _categoryId = categoryId;
            _description = description;
        }
        public override void Execute()
        {
            _facade.CreateOperation(_type, _bankAccountId, _amount, _date, _categoryId, _description);
        }

        public override void Undo()
        {
            _facade.CancelOperation(_createdOperationId);
        }
    }
}
