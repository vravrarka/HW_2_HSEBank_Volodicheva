using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Repositories
{
    public class DatabaseBankAccountRepository : IBankAccountRepository
    {
        public BankAccount GetById(int id)
        {
            Thread.Sleep(100);
            return new BankAccount { Id = id, Name = $"Счет из БД {id}", Balance = id * 1000 };
        }
        public IEnumerable<BankAccount> GetAll()
        {
            Thread.Sleep(500);
            return new List<BankAccount>
            {
                new BankAccount { Id = 1, Name = "Основной счет", Balance = 10000 },
                new BankAccount { Id = 2, Name = "Накопительный счет", Balance = 50000 }
            };
        }
        public void Save(BankAccount account)
        {
            Thread.Sleep(100);
        }
        public void Delete(int id)
        {
            Thread.Sleep(100);
        }
    }
}
