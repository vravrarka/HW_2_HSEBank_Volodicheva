using Bank.App.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.MainClasses
{
    public class BankAccount : ObjectId, IVisitable
    {  
        public string Name { get; set; }
        public int Balance { get; set; }
        public List<Operation> Operations { get; set; } = new List<Operation>();
        public void Accept(DataVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
