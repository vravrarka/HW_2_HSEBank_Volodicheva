using Bank.App.Repositories;
using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Facade
{
    public class BankAccountFacade 
    {
        private readonly DataRepository _repository;
        public BankAccountFacade()
        {
            _repository = DataRepository.Instance;
        }
        public void AddAccount(BankAccount account)
        {
            _repository.BankAccounts.Add(account);
            Console.WriteLine($"Добавлен счет: {account.Name} с балансом {account.Balance}");
        }
        public BankAccount GetBankAccount(int id)
        {
            return _repository.BankAccounts.FirstOrDefault(a => a.Id == id);
        }
        public IEnumerable<BankAccount> GetAllBankAccounts()
        {
            return _repository.BankAccounts;
        }
        public void UpdateAccount(int id, int amount)
        {
            var account = GetBankAccount(id);
            if (account != null)
            {
                account.Balance += amount;
                Console.WriteLine($"Баланс счета {account.Name} изменен на: {amount}");
            }
        }
        public void DeleteAccount(int id)
        {
            var account = GetBankAccount(id);
            _repository.BankAccounts.Remove(account);
            Console.WriteLine($"Удален счет {account.Name}");
        }
        public void DisplayAllAccounts()
        {
            Console.WriteLine("\nВсе счета:\n");
            foreach (var account in _repository.BankAccounts)
            {
                Console.WriteLine($"ID: {account.Id}, Название: {account.Name}, Баланс: {account.Balance}");
            }
        }
    }
}
