using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.App.Facade;

namespace Bank.App.Commands
{
    public class CreateAccountCommand : Command
    {
        private readonly FinancialAccountingFacade _facade;
        private readonly string _name;
        private readonly int _initialBalance;
        private int _createdAccountId;
        public CreateAccountCommand(FinancialAccountingFacade facade, string name, int initialBalance = 0)
        {
            _facade = facade;
            _name = name;
            _initialBalance = initialBalance;
        }
        public override void Execute()
        {
            _facade.CreateAccount(_name, _initialBalance);
        }
        public override void Undo()
        {
            _facade.DeleteAccount(_createdAccountId);
        }
    }
}
