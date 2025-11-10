using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.MainClasses;

namespace Bank.App.Factory
{
    public class BankAccountCreator : ObjectCreator
    {  
        private readonly string _name;
        private readonly int _initialBalance;
        public BankAccountCreator(string name, int initialBalance = 0)
        {
            _name = name;
            _initialBalance = initialBalance;
        }
        public override ObjectId FactoryMethod()
        {
            if (string.IsNullOrWhiteSpace(_name)) throw new ArgumentNullException("Account name cannot be empty");
            return new BankAccount
            {
                Name = _name,
                Balance = _initialBalance
            };

        }
    }
}
