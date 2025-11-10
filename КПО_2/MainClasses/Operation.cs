using Bank.App.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.MainClasses
{

    public class Operation : ObjectId, IVisitable
    {
        public CategoryType Type { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public DateOnly Date {  get; set; }
        public int CategoryId { get; set; }
        public int BankAccountId { get; set; }
        public void Accept(DataVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}