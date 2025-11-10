using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Repositories
{
    public interface IBankAccountRepository
    {
        BankAccount GetById(int id);
        IEnumerable<BankAccount> GetAll();
        void Save(BankAccount account);
        void Delete(int id);
    }
}
