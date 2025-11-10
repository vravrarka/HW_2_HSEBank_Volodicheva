using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Repositories
{
    public class DataRepository
    {
        private static DataRepository _instance;
        public static DataRepository Instance => _instance ??= new DataRepository();

        public List<BankAccount> BankAccounts { get; } = new List<BankAccount>();
        public List<Category> Categories { get; } = new List<Category>();
        public List<Operation> Operations { get; } = new List<Operation>();

        private DataRepository() { }

        public void ClearAll()
        {
            BankAccounts.Clear();
            Categories.Clear();
            Operations.Clear();
        }
    }
}