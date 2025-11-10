using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Export
{
    public abstract class DataVisitor
    {
        public abstract void Visit(BankAccount account);
        public abstract void Visit(Category category);
        public abstract void Visit(Operation operation);

    }
}
